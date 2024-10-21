import { Component, OnInit } from '@angular/core';
import { CandidateService, Candidate } from '../candidate.service';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.css']
})
export class CandidateListComponent implements OnInit {

  candidates: Candidate[] = [];

  constructor(private candidateService: CandidateService) { }

  ngOnInit(): void {
    this.getCandidates();
  }

  getCandidates(): void {
    this.candidateService.getCandidates().subscribe(candidates => {
      this.candidates = candidates;
    });
  }

  editCandidate(id: number): void {
    // Navigate to the edit page (to be implemented later)
    // e.g., this.router.navigate(['/candidate-edit', id]);
  }

  deleteCandidate(id: number): void {
    this.candidateService.deleteCandidate(id).subscribe(() => {
      this.candidates = this.candidates.filter(p => p.cId !== id);  // Remove deleted candidate from the list
    });
  }

}
