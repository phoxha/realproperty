import { Component, HostBinding } from '@angular/core';
import { TranslationService } from './services/translation.service';
// language list
import { locale as enLang } from './config/i18n/en';
import { locale as chLang } from './config/i18n/ch';
import { locale as esLang } from './config/i18n/es';
import { locale as jpLang } from './config/i18n/jp';
import { locale as deLang } from './config/i18n/de';
import { locale as frLang } from './config/i18n/fr';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'body[m-root]',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'realproperty';

  @HostBinding('style') style: any;
  @HostBinding('class') classes: any = '';

  constructor(private translationService: TranslationService) {
  // register translations
  this.translationService.loadTranslations(enLang, chLang, esLang, jpLang, deLang, frLang);
  }
}
