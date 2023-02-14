import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'myeshop';
  public keyword = "";
  public isAuthorized = false;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    ) { }

  ngOnInit() {
    this.oidcSecurityService
      .checkAuth()
      .subscribe((auth) => 
        this.isAuthorized = auth.isAuthenticated);
  }


  public send(keyword: string){
    this.keyword = keyword;
  }
}
