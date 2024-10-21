import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Test {
  tId: number;
  tName: string;
  tDescription: string;
  tUpdateDate: string | Date;
  tStatus: boolean;
 }

@Injectable({
  providedIn: 'root',
})

export class TestService {
  private apiUrl = 'https://localhost:44400/admin/test';

  constructor(private http: HttpClient) { }

  getTests(): Observable<Test[]> {
    return this.http.get<Test[]>(this.apiUrl);
  }

  deleteTest(testId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${testId}`);
  }

  getTestById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  addTest(test: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, test);
  }

  updateTest(test: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${test.tId}`, test);
  }
}
