import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ProjectService } from './../../../app/Services/projectService';
import { Project } from 'src/app/Model/ProjectModel';
import { AuthService, ConfigStateService } from '@abp/ng.core';
import { DatePipe } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormBuilder, Validators } from '@angular/forms';
import { UserProjectService } from 'src/app/Services/userprojectService';

@Component({
  standalone: true,
  selector: 'all-project-route',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
  imports: [SharedModule, RouterLink, DatePipe, RouterOutlet],
})
export class ProjectsComponent implements OnInit {
  //variable
  isModalOpen: boolean = false;
  allProjectData: Array<Project> = [];
  userProject: Array<any> = [];
  projectData: Array<Project> = [];
  selectedTab: string = 'All Progress';
  selectedProject: any;
  projectForm = this.fb.group({
    status: ['', Validators.required],
    member: ['', Validators.required],
  });
  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService,
    private authService: AuthService,
    private userProjectService: UserProjectService,
    private config: ConfigStateService
  ) {}
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  login(): void {
    this.authService.navigateToLogin();
  }

  filterData(status: string): any {
    return this.allProjectData.filter(project => project?.status == status);
  }
  selectTab(tabId: string): void {
    this.selectedTab = tabId;
    if (tabId == 'All Progress') {
      console.log(this.allProjectData);
      this.projectData = this.allProjectData;
    } else if (tabId == 'In Progress') {
      this.projectData = this.allProjectData.filter(project => project?.status == 'inprogress');
    } else if (tabId == 'Hold') {
      this.projectData = this.allProjectData.filter(project => project?.status == 'hold');
    } else {
      this.projectData = this.allProjectData.filter(project => project?.status == 'closed');
    }
    console.log(this.projectData);
  }

  handleEdit(id): void {
    this.isModalOpen = true;
    this.selectedProject = this.allProjectData.filter(project => project.id == id)[0];
    this.projectForm.patchValue({
      status: this.selectedProject.status,
      member: this.selectedProject.member,
    });

    console.log(this.selectedProject);
  }

  handleUpdateProject(): void {
    console.log('working');
    this.projectService
      .updateProject(this.selectedProject.id, {
        name: this.selectedProject.name,
        description: this.selectedProject.description,
        projectManager: this.selectedProject.projectManager,
        status: this.projectForm.value.status,
        member: this.projectForm.value.member.toString(),
      })
      .subscribe(
        d => {
          const index = this.allProjectData.findIndex(
            project => project.id === this.selectedProject.id
          );
          if (index !== -1) {
            this.allProjectData[index].status = this.projectForm.value.status;
            this.allProjectData[index].member = this.projectForm.value.member;
          }
          this.isModalOpen = false;
        },
        error => {
          alert('Error while delete');
        }
      );
  }
  handleDelete(id): void {
    this.projectService.deleteProject(id).subscribe(d => {});
    this.allProjectData = this.allProjectData.filter(item => (item.id = id));
  }
  ngOnInit(): void {
    const currentUser = this.config.getOne('currentUser');
    this.userProjectService.getAllItem().subscribe(data => {
      this.userProject = data?.filter(item => item.usersId == currentUser?.id);
    });

    this.projectService.getAllProjects().subscribe(
      data => {
        const userProjects = [];
        if (!(currentUser.roles[0] == 'admin' || currentUser.roles[0] == 'auditor')) {
          console.log(data.length,this.userProject.length);
          for (const project of data) {
            for (const userProject of this.userProject) {
              console.log(project,userProject);
              if (project.id == userProject.projectId) {
                userProjects.push(project);
              }
              else{
                console.log("no");
              }
            }
          }
          this.allProjectData = userProjects;
          this.projectData = userProjects;
        } else {
          this.allProjectData = data;
          this.projectData = data;
        }
      },
      error => {
        console.error('Error fetching projects:', error);
      }
    );
  }
}
