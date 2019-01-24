import { Component, OnInit, Input, ChangeDetectionStrategy } from '@angular/core';
import { SpinnerButtonOptions } from './button-options.interface';

@Component({
// tslint:disable-next-line:component-selector
selector: 'm-spinner-button',
templateUrl: './spinner-button.component.html',
styleUrls: ['./spinner-button.component.scss'],
})
export class SpinnerButtonComponent implements OnInit {
@Input() options: SpinnerButtonOptions;

constructor() {}

ngOnInit() {}
}
