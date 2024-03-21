import { AuthService, ConfigStateService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Router, RouterOutlet } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProjectService } from '../../Services/projectService';
import { Project } from '../../Model/ProjectModel';
import { IdentityRoleService, IdentityUserService } from '@abp/ng.identity/proxy';
import { UserService } from '../../Services/userService';
import { UserProjectService } from '../../Services/userprojectService';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './app-project.componet.html',
  imports: [SharedModule, RouterOutlet, ReactiveFormsModule],
})
export class AddProjectComponent {
  allUsers: any;
  clients=[];
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
    private projectService: ProjectService,
    private fb: FormBuilder,
    private userService: UserService,
    private identityUser: IdentityUserService,
    private identityRole: IdentityRoleService,
    private userProjectService: UserProjectService,
    private router:Router
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
                email: user?.email,
              });
            }
            else if(userRole.items[0].name == 'client'){
                this.clients.push({
                    name: user?.name,
                    id: user?.id,
                    email: user?.email,
                  });
            }
          });
        });
      });

    this.identityRole.getAllList().subscribe(d => {
      console.log(d);
    });
  }

  ngOnInit(): void {
    this.isModalOpen=true;
  }

  nextStage() {
    this.currentStage++;
  }

  prevStage() {
    this.currentStage--;
  }
  ngSubmit() {
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

    console.log(this.clients);
    this.projectService.createProject(modalData).subscribe(data => {
      console.log(data);
      //check client with same id exist then 
      const existedClient:any=this.clients.filter(item=>item.email==this.clientForm.value.clientEmail);
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
          usersId: existedClient[0].id,
        }).subscribe((d)=>{});
      });
      currentManagers.forEach(e => {
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
              usersId: e.id,
            }).subscribe((d)=>{})
          });
      });
      this.isModalOpen = false;
      this.router.navigate(['/']);
    });
  }
}
