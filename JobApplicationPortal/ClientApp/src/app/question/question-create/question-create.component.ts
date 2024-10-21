import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { QuestionService } from '../question.service';
import { TestService } from '../../test/test.service';

@Component({
  selector: 'app-question-create',
  templateUrl: './question-create.component.html',
  styleUrls: ['./question-create.component.css']
})
export class QuestionCreateComponent {
  questionForm: FormGroup;
  tests: any[] = [];

  constructor(
    private questionService: QuestionService,
    private testService: TestService,
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
  }

  loadTests() {
    this.testService.getTests().subscribe(data => {
      this.tests = data;
    });
  }

  saveQuestion() {
    this.questionService.addQuestion(this.questionForm.value).subscribe(() => {
      this.router.navigate(['/admin/questions']);
    });
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
