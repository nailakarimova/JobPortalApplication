import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface options {
  answerId: number,
  answerText: string,
  isCorrectOption: boolean
}

export interface Quiz {
  questionId: number;
  questionText: string;
  options: options[]
}

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private apiUrl = 'https://localhost:44400/quiz';

  constructor(private http: HttpClient) { }

  getQuizs(testId: number): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${this.apiUrl}/${testId}`);
  }

  submitQuiz(quizData: { candidateId: number, answers: { questionId: number, answerId: number }[] }): Observable<any> {
    return this.http.post('/quiz/submitQuiz', quizData);
  }

}
