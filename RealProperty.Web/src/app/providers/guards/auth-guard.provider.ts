import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LocalStorageService } from '../local-storage.service';
import { LocalStorageKeyEnum } from 'src/app/enums/local-storage-key.enum';

@Injectable()
export class AuthGuardProvider implements CanActivate {

  constructor(
    private router: Router,
    private localStorage: LocalStorageService
  ) { }

  canActivate() {
      if (this.localStorage.getObject(LocalStorageKeyEnum.User)) {
          return true;
      }

      this.router.navigate(['/login']);
      return false;
  }
}
