import { Component, OnInit, HostBinding, ViewChild, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/login.model';
import { AuthenticationService } from '../../authentication.service';
import { SpinnerButtonOptions } from 'src/app/components/partials/spinner-button/button-options.interface';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @HostBinding('class') classes = 'm-login__signin';

  @ViewChild('form') form: NgForm;

  public model: LoginModel = new LoginModel();

  spinner: SpinnerButtonOptions = {
    active: false,
    spinnerSize: 18,
    raised: true,
    buttonColor: 'primary',
    spinnerColor: 'accent',
    fullWidth: false
  };

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) { }

  login() {
    this.spinner.active = true;
    this.authService.login(this.model).subscribe(() => {
      this.router.navigateByUrl(this.authService.getInterruptedUrl());
      this.spinner.active = false;
    });
  }

  ngOnInit() {
  }
}
