import { Component } from '@angular/core';
import { ReplaceableComponentsService } from '@abp/ng.core';
import { AppLayoutComponent } from './components/app-layout/app-layout.component';
import { eThemeLeptonXComponents } from '@abp/ng.theme.lepton-x';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  `,
})
export class AppComponent {
  constructor(
    private replaceableComponents: ReplaceableComponentsService // injected the service
  ) {
    this.replaceableComponents.add({
      component: AppLayoutComponent,
      key: eThemeLeptonXComponents.ApplicationLayout,
    });
  }
}
// identity/roles
// identity/users
// account/manage