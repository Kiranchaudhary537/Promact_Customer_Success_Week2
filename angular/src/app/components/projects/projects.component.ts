import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ProjectService } from './../../../app/Services/projectService';
import { Project } from 'src/app/Model/ProjectModel';
import { AuthService } from '@abp/ng.core';
import { DatePipe } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'all-project-route',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
  imports: [SharedModule,RouterLink, DatePipe,RouterOutlet],
})
export class ProjectsComponent implements OnInit {
  //variable
  isModalOpen:boolean=false;
  allProjectData: Array<Project> = [];
  projectData: Array<Project> = [];
  selectedTab: string = 'All Progress';
  selectedProject:any;
  projectForm = this.fb.group({
    status: ['', Validators.required],
    member: ['', Validators.required],
  });
  constructor(private fb:FormBuilder, private projectService: ProjectService, private authService: AuthService) {}
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  login(): void {
    this.authService.navigateToLogin();
  }

  filterData(status:string):any{
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

  handleEdit(id):void{
      this.isModalOpen=true;
      this.selectedProject=this.allProjectData.filter(project => project.id==id)[0];
      this.projectForm.patchValue({
        status: this.selectedProject.status,
        member: this.selectedProject.member
      });
      
      console.log(this.selectedProject);
  }

  handleUpdateProject():void{
    console.log("working");
    this.projectService.updateProject(this.selectedProject.id,{
      name:this.selectedProject.name,
      description:this.selectedProject.description,
      projectManager:this.selectedProject.projectManager,
      status:this.projectForm.value.status,
      member:this.projectForm.value.member.toString()
    }).subscribe((d)=>{
      this.isModalOpen=false;
    },
    (error)=>{
      alert("Error while delete");
    })
  }
  handleDelete(id):void{
    this.projectService.deleteProject(id).subscribe((d)=>{
      this.allProjectData=this.allProjectData.filter(item=>item.id=id);
    });
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

