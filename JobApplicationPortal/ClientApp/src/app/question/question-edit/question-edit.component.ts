import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { QuestionService } from '../question.service';
import { TestService } from '../../test/test.service';

@Component({
  selector: 'app-question-edit',
  templateUrl: './question-edit.component.html',
  styleUrls: ['./question-edit.component.css']
})

export class QuestionUpdateComponent implements OnInit {
  questionForm: FormGroup;
  tests: any[] = [];
  isEditMode: boolean = true;

  constructor(
    private questionService: QuestionService,
    private testService: TestService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.questionForm = this.fb.group({
      question_text: [''],
      question_test_id: [null],
      answers: this.fb.array([])
    });
  }

  ngOnInit(): void {
    this.loadTests();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadQuestion(id);
    }
  }

  loadTests() {
    this.testService.getTests().subscribe(data => {
      this.tests = data;
    });
  }

  loadQuestion(id: string) {
    this.questionService.getQuestionById(+id).subscribe(data => {
      this.questionForm.patchValue(data);
      this.setAnswers(data.answers);
    });
  }

  setAnswers(answers: any[]) {
    const answersArray = this.questionForm.get('answers') as FormArray;
    answersArray.clear(); // Clear the existing answers
    answers.forEach(answer => {
      answersArray.push(this.fb.group({
        answer_text: [answer.answer_text || ''],
        is_correct: [answer.is_correct || false]
      }));
    });
  }

  updateQuestion() {
    const questionId = this.route.snapshot.paramMap.get('id');
    if (questionId) {
      this.questionService.updateQuestion(+questionId, this.questionForm.value).subscribe(() => {
        this.router.navigate(['/admin/questions']);
      });
    }
  }

  get answers(): FormArray {
    return this.questionForm.get('answers') as FormArray;
  }

  addAnswer() {
    this.answers.push(this.fb.group({
      answer_text: [''],
      is_correct: [false]
    }));
  }

  removeAnswer(index: number) {
    this.answers.removeAt(index);
  }
}
