import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AzerbaijaniPhoneNumberService } from 'src/azerbaijani-phone-number.service';
import { Candidate, CandidateService } from '../candidate.service';

@Component({
  selector: 'app-candidate-create',
  templateUrl: './candidate-create.component.html', // Adjust the template path as necessary
  styleUrls: ['./candidate-create.component.css']
})
export class CandidateCreateComponent implements OnInit {
  jId: number | null = null;

  candidate: Candidate = {
    cId: 0, // Initialize to zero or leave it for an API to assign
    cFirstname: '',
    cLastname: '',
    cEmail: '',
    cPhone: '',
  };
  phoneError: string = '';

  constructor(private phoneNumberService: AzerbaijaniPhoneNumberService, private candidateService: CandidateService, private router: Router, private route: ActivatedRoute) { }
    ngOnInit() {
      this.jId = +this.route.snapshot.paramMap.get('jId')!;
      console.log("this.jId: " + this.jId)
    }

  validatePhone() {
    if (!this.phoneNumberService.checkIfValidAzerbaijaniPhoneNumber(this.candidate.cPhone)) {
      this.phoneError = 'Invalid Azerbaijani phone number. Must start with 50, 51, 55, 70, 77, 99, or 10 and have 7 more digits.';
    } else {
      this.phoneError = ''; // Clear error if valid
    }
  }

  onSubmit(form: any): void {
    if (form.valid && !this.phoneError) {
      this.candidateService.createCandidate(this.candidate).subscribe({
        next: (response) => {
          console.log('Candidate created successfully', response);// i need response.cId
          const candidateId = response.cId;
          console.log('Candidate ID:', candidateId);
          // Optional: Navigate to another component (e.g., candidate list)
          this.router.navigate(['/quiz', candidateId, this.jId]);
          
        },
        error: (err) => {
          console.error('Error creating candidate', err);
        }
      });
    } else {
      console.log('Form is invalid or phone number is incorrect');
    }
  }
}
