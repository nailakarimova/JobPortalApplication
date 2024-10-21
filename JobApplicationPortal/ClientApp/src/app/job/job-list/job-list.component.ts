import { Component, OnInit } from '@angular/core';
import { JobService, Job } from '../job.service';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.css']
})
export class JobListComponent implements OnInit {

  jobs: Job[] = [];

  constructor(private jobService: JobService) { }

  ngOnInit(): void {
    this.getJobs();
  }

  getJobs(): void {
    this.jobService.getJobs().subscribe(jobs => {
      this.jobs = jobs;
    });
  }

  editJob(id: number): void {
    // Navigate to the edit page (to be implemented later)
    // e.g., this.router.navigate(['/job-edit', id]);
  }

  deleteJob(id: number): void {
    this.jobService.deleteJob(id).subscribe(() => {
      this.jobs = this.jobs.filter(p => p.jId !== id);  // Remove deleted job from the list
    });
  }

}
