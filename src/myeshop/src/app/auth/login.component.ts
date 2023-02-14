import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  constructor(
    public oidcSecurityService: OidcSecurityService,
    public http: HttpClient) { }

  ngOnInit() {
    this.oidcSecurityService
      .checkAuth()
      .subscribe((auth) => console.log('is authenticated', auth));
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  callApi() {
    const token = this.oidcSecurityService.getAccessToken();

    this.http.get("http://localhost:5002/secret", {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token,
      }),
      responseType: 'text'
    })
      .subscribe((data: any) => {
        console.log("api result:", data);
      });
  }
}
