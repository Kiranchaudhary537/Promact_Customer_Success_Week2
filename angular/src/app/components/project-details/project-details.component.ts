import { AuthService, ConfigStateService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterOutlet } from '@angular/router';
import { GeneratePdfService } from 'src/app/Services/generatePdfService';

@Component({
  standalone: true,
  selector: 'project-route',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss'],
  imports: [RouterLink, RouterOutlet],
})
export class ProjectDetailComponent {
  id: string;
  projectId: string = '';
  constructor(private generatePdfService:GeneratePdfService,
    private config:ConfigStateService,
    private route: ActivatedRoute) {
    this.projectId = this.route.snapshot.pathFromRoot[2].params['id'];
  }
  convertBase64ToPDF(base64String, filename = 'output.pdf'):void {
    const linkSource = `data:application/pdf;base64,${base64String}`;
    const downloadLink = document.createElement("a");
    downloadLink.href = linkSource;
    downloadLink.download = filename;
    downloadLink.click();
  }
  generatePdf():void{
    this.generatePdfService.getPdf(this.projectId).subscribe((data)=>{
      this.convertBase64ToPDF(data);
    })
  }
}
