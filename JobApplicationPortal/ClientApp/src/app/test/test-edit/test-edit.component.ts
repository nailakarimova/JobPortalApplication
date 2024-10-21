import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestService } from '../test.service'; 

@Component({
  selector: 'app-test-edit',
  templateUrl: './test-edit.component.html',
  styleUrls: ['./test-edit.component.css']
})

export class TestUpdateComponent implements OnInit {
  test = {
    tId: null,        
    tName: '',
    tDescription: '',
    tStatus: true
  };

  constructor(
    private testService: TestService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    const testId = this.route.snapshot.paramMap.get('id');
    if (testId) {
      this.getTestDetails(testId);
    }
  }

  getTestDetails(id: string) {
    this.testService.getTestById(id).subscribe((data) => {
      this.test = data;
    });
  }

  onSubmit() {
    this.testService.updateTest(this.test).subscribe(() => {
      this.router.navigate(['/admin/test-list']); 
    });
  }
}

