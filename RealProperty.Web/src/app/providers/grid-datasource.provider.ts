import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/common/api-response.model';
import { SearchResult } from '../models/common/search-results.model';
import CustomStore from 'devextreme/data/custom_store';

@Injectable()
export class GridDataSourceProvider {

  get<T>(gridResult: (params: string) => Observable<ApiResponse<SearchResult<T>>>, key: any = 'id'): any {

    const classScope = this;

    return {
      store: new CustomStore({
        load(loadOptions) {

          const params = [];

          if (loadOptions.skip != null) {
              params.push(`skip=${loadOptions.skip}`);
          }

          if (loadOptions.take != null) {
              params.push(`take=${loadOptions.take}`);
          }

          if (loadOptions.sort) {
              params.push(`orderby=${(loadOptions.sort as any[])
                  .map(sort => sort.selector + (sort.desc ? ' desc' : '')).join(',')}`);
          }

          const paramsQuery = `?${params.join('&')}`;

          return gridResult(paramsQuery).toPromise().then((response: ApiResponse<SearchResult<T>>) => response.model);
        },
        key: key
      })
    };
  }
}