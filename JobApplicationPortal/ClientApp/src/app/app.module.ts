import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { JobListComponent } from './job/job-list/job-list.component';
import { JobCreateComponent } from './job/job-create/job-create.component';
import { JobEditComponent } from './job/job-edit/job-edit.component';

import { CandidateListComponent } from './application-form/candidate-list/candidate-list.component';
import { CandidateCreateComponent } from './application-form/candidate-create/candidate-create.component';
import { CandidateEditComponent } from './application-form/candidate-edit/candidate-edit.component';

import { QuizComponent } from './quiz/quiz.component';

import { TestCreateComponent } from './test/test-create/test-create.component';
import { TestUpdateComponent } from './test/test-edit/test-edit.component';
import { TestListComponent } from './test/test-list/test-list.component';

import { QuestionListComponent } from './question/question-list/question-list.component';
import { QuestionCreateComponent } from './question/question-create/question-create.component';
import { QuestionUpdateComponent } from './question/question-edit/question-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    JobListComponent,
    CandidateListComponent,
    CandidateCreateComponent,
    QuizComponent,
    TestListComponent,
    TestCreateComponent,
    TestUpdateComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'job-list', component: JobListComponent },
      { path: 'candidate-create/:jId', component: CandidateCreateComponent }, 
      { path: 'quiz/:candidateId/:jobId', component: QuizComponent },
      { path: 'admin/test-list', component: TestListComponent },
      { path: 'admin/test/create', component: TestCreateComponent },
      { path: 'admin/test-update/:id', component: TestUpdateComponent },
      { path: 'admin/question', component: QuestionListComponent },
      { path: 'admin/question/create', component: QuestionCreateComponent }, 
      { path: 'admin/question-update/:id', component: QuestionUpdateComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
