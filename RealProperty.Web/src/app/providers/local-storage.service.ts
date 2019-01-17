import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageService {

  constructor() { }

  getObject<T>(key: string): T {
    this.throwExceptionIfKeyUndefined(key);
    const item = window.localStorage.getItem(key);
    if (!item) {
      return null;
    }
    return JSON.parse(item) as T;
  }

  private throwExceptionIfKeyUndefined(key: string) {
    if (!key) {
        throw new Error('Key is undefined');
    }
  }
}
