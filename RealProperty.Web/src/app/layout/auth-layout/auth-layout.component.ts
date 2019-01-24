import { Component, OnInit, HostBinding } from '@angular/core';


@Component({
  // tslint:disable-next-line:component-selector
  selector: 'auth-layout',
  templateUrl: './auth-layout.component.html',
  styleUrls: ['./auth-layout.component.scss']
})
export class AuthLayoutComponent implements OnInit {
	@HostBinding('id') id = 'm_login';
	@HostBinding('class')
	// tslint:disable-next-line:max-line-length
  classses: any = 'm-grid m-grid--hor m-grid--root m-page';

  constructor() { }

  ngOnInit() {
  }

}
