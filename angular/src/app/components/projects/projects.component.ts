import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ProjectService } from './../../../app/Services/projectService';
import { Project } from 'src/app/Model/ProjectModel';
import { AuthService } from '@abp/ng.core';
import { GeneratePdfService } from 'src/app/Services/generatePdfService';

@Component({
  standalone: true,
  selector: 'all-project-route',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
  imports: [RouterLink],
})
export class ProjectsComponent implements OnInit {
  //variable
  allProjectData: Array<Project> = [];
  projectData: Array<Project> = [];
  selectedTab: string = 'allProject';
  constructor(private projectService: ProjectService, private authService: AuthService) {}
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  login(): void {
    this.authService.navigateToLogin();
  }

  selectTab(tabId: string): void {
    this.selectedTab = tabId;
    if (tabId == 'all') {
      console.log(this.allProjectData)
      this.projectData = this.allProjectData;
    } else if (tabId == 'inprogress') {
      this.projectData = this.allProjectData.filter(project => project?.status == 'inprogress');
    } else if (tabId == 'hold') {
      this.projectData = this.allProjectData.filter(project => project?.status == 'hold');
    } else {
      this.projectData = this.allProjectData.filter(project => project?.status == 'closed');
    }
    console.log(this.projectData);
  }

  ngOnInit(): void {
    this.projectService.getAllProjects().subscribe(
      data => {
        console.log('Projects:', data);
        this.allProjectData = data;
        this.projectData = data;
      },
      error => {
        console.error('Error fetching projects:', error);
      }
    );
  }
}
