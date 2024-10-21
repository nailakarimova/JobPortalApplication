import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TestService } from '../test.service'; 

@Component({
  selector: 'app-test-create',
  templateUrl: './test-create.component.html',
  styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent {
  test = {
    tName: '',
    tDescription: '',
    tStatus: true
  };

  constructor(private testService: TestService, private router: Router) { }

  onSubmit() {
    this.testService.addTest(this.test).subscribe(() => {
      this.router.navigate(['/tests']); 
    });
  }
}
