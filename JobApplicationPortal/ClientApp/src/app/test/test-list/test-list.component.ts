import { Component, OnInit } from '@angular/core';
import { TestService } from '../test.service'; 
import { ActivatedRoute, Router } from '@angular/router';

interface Test {
  tId: number;
  tName: string;
  tDescription: string;
  tUpdateDate: string | Date;
  tStatus: boolean;
}

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})

export class TestListComponent implements OnInit {
  tests: Test[] = [];
  isLoading = true;
  errorMessage: string | null = null;

  constructor(private testService: TestService, private router: Router) { }

  ngOnInit(): void {
    this.loadTests();
  }

  loadTests(): void {
    this.testService.getTests().subscribe({
      next: (data) => {
        this.tests = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load tests. Please try again later.';
        this.isLoading = false;
      }
    });
  }

  deleteTest(testId: number): void {
    if (confirm('Are you sure you want to delete this test?')) {
      this.testService.deleteTest(testId).subscribe({
        next: () => {
          this.tests = this.tests.filter(test => test.tId !== testId);
        },
        error: () => {
          this.errorMessage = 'Failed to delete the test. Please try again later.';
        }
      });
    }
  }

  navigateToUpdate(testId: number) {
    this.router.navigate(['/admin/test-update', testId]); 
  }

}
