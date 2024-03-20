import { AuthService, ConfigStateService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProjectService } from '../Services/projectService';
import { Project } from '../Model/ProjectModel';
import { IdentityRoleService, IdentityUserService } from '@abp/ng.identity/proxy';
import { UserService } from '../Services/userService';
import { UserProjectService } from '../Services/userprojectService';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [SharedModule, RouterOutlet, ReactiveFormsModule],
})
export class HomeComponent {
  allUsers: any;
  clients: Array<any>;
  isModalOpen: boolean;
  inProgress: boolean;
  currentStage: number = 1;
  clientForm = this.fb.group({
    clientName: ['', [Validators.required]],
    clientEmail: ['', [Validators.required, Validators.email]],
  });
  managerForm = this.fb.group({
    manager: ['', [Validators.required]],
  });
  managers = [];
  projectForm = this.fb.group({
    name: ['', [Validators.required]],
    description: ['', [Validators.required]],
  });

  constructor(
    private authService: AuthService,
    private projectService: ProjectService,
    private fb: FormBuilder,
    private userService: UserService,
    private identityUser: IdentityUserService,
    private identityRole: IdentityRoleService,
    private userProjectService: UserProjectService
  ) {
    this.identityUser
      .getList({
        maxResultCount: 100,
      })
      .subscribe(data => {
        data.items.forEach(user => {
          this.identityUser.getRoles(user.id).subscribe(userRole => {
            console.log(userRole.items[0]);
            if (userRole.items[0].name == 'projectManager') {
              this.managers.push({
                name: user?.name,
                id: user?.id,
                email:user?.email,
              });
            }
          });
        });
      });

    this.identityRole.getAllList().subscribe(d => {
      console.log(d);
    });
  }
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  ngOnInit(): void {}

  nextStage() {
    this.currentStage++;
  }

  prevStage() {
    this.currentStage--;
  }
  ngSubmit() {
    console.log(this.projectForm.value);
    console.log(this.managerForm.value);
    console.log(this.clientForm.value);
    if (this.projectForm.invalid) return;
    if (this.currentStage === 1 && this.clientForm.invalid) return;
    if (this.currentStage === 2 && this.managerForm.invalid) return;
    console.log(this.projectForm.value);

    const modalData: Project = {
      name: this.projectForm.value.name,
      description: this.projectForm.value.description,
      projectManager: this.managerForm.value.manager,
      member: '0',
      status: 'inprogress',
    };
    //get user
    const currentManagers = this.managers.filter(
      item => item.name == this.managerForm.value.manager
    );

    this.projectService.createProject(modalData).subscribe(data => {
      this.isModalOpen = false;
      console.log(data);
      this.userService
        .createItem({
          name: this.clientForm.value.clientName,
          email: this.clientForm.value.clientEmail,
          role: 'client',
        })
        .subscribe(res => {
          console.log(res);
          this.userProjectService.createItem({
            projectId: data.id,
            usersId: res.id,
          });
        });
      currentManagers.forEach(e => {
        console.log(e);
        this.userService
          .createItem({
            name: e?.name,
            email: e.email,
            role: 'projectManager',
          })
          .subscribe(res => {
            console.log(res);
            this.userProjectService.createItem({
              projectId: data.id,
              usersId: res.id,
            });
          });
      });
    });
  }
}
