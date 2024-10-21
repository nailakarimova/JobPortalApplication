import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService, Quiz } from '../quiz/quiz.service';

interface Question {
  questionId: number;
  questionText: string;
  options: { answerId: number, answer: string }[];
}

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})

export class QuizComponent implements OnInit {
  candidateId: number | null = null;
  jobId: number | null = null;

  showModal = true;
  timePerQuestion = 60;
  timeLeft!: number;
  timerId: any;
  currentQuestionIndex = 0;
  quizs: Quiz[] = [];
  currentQuestion: Quiz | null = null;

  questions: Question[] = [];
  selectedAnswers: { questionId: number, answerId: number }[] = [];
  isLoading = true;
  quizCompleted = false;

  testId: number = 1;

  /****** CV file details *****/
  selectedFile: File | null = null;
  uploadSuccess: boolean = false;
  uploadError: string | null = null;

  constructor(private quizService: QuizService, private route: ActivatedRoute, private http: HttpClient, private router: Router) { }


  ngOnInit() {
    this.candidateId = +this.route.snapshot.paramMap.get('candidateId')!;
    this.jobId = +this.route.snapshot.paramMap.get('jobId')!;

    this.loadQuestions();
  }

  loadQuestions() {
    this.quizService.getQuizs(this.testId).subscribe(
      (data: Quiz[]) => {
        this.quizs = data;
        this.currentQuestion = this.quizs[this.currentQuestionIndex];
        this.isLoading = false;
      },
      (error) => {
        console.error('Error loading questions', error);
        this.isLoading = false; 
      }
    );
  }

  selectAnswer(questionId: number, answerId: number) {
    const existingAnswerIndex = this.selectedAnswers.findIndex(a => a.questionId === questionId);
    if (existingAnswerIndex !== -1) {
      this.selectedAnswers[existingAnswerIndex].answerId = answerId;
    } else {
      this.selectedAnswers.push({ questionId, answerId });
    }
  }


  closeModal() {
    this.showModal = false;
    if (!this.isLoading) { // Only start timer if questions are loaded
      this.startQuiz();
    }
  }

  startQuiz() {
    this.startTimer();
  }

  startTimer() {
    this.timeLeft = this.timePerQuestion;
    this.timerId = setInterval(() => {
      this.timeLeft--;
      if (this.timeLeft <= 0) {
        clearInterval(this.timerId);
        this.nextQuestion();
      }
    }, 1000);
  }

  nextQuestion() {
    clearInterval(this.timerId);
    if (this.currentQuestionIndex < this.quizs.length - 1) {
      this.currentQuestionIndex++;
      this.currentQuestion = this.quizs[this.currentQuestionIndex];
      this.startTimer();
    } else {
      this.completeQuiz();
      this.quizCompleted = true;
    }
  }

  completeQuiz() {
    this.quizCompleted = true;
    this.submitQuizResults();
  }

  submitQuizResults() {
    if (this.candidateId) {
      const quizData = {
        TestId: this.testId,
        candidateId: this.candidateId,
        answers: this.selectedAnswers
      };
      this.quizService.submitQuiz(quizData).subscribe(response => {
        console.log('Quiz results saved successfully');
      }, error => {
        console.error('Error saving quiz results:', error);
      });
    }
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      const validExtensions = ['application/pdf', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'];
      if (file.size > 5 * 1024 * 1024) {
        this.uploadError = 'File size must be less than 5MB.';
      } else if (!validExtensions.includes(file.type)) {
        this.uploadError = 'Only PDF or DOCX files are allowed.';
      } else {
        this.selectedFile = file;
        this.uploadError = null;
      }
    }
  }

  onSubmit() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append('resume', this.selectedFile);

      const candidateId = this.candidateId;

      this.http.post(`/upload?candidateId=${candidateId}`, formData).subscribe({
        next: (response: any) => {
          this.uploadSuccess = true;
          this.uploadError = null;
          setTimeout(() => {
            this.router.navigate(['/job-list']);
          }, 2000); 
        },
        error: (err) => {
          this.uploadSuccess = false;
          this.uploadError = 'Failed to upload CV. Please try again.';
        }
      });
    }
  }

}
