import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
@Injectable()
export class FishService {
  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }
  getFishes(tankId) {
    return this._http.get(this.myAppUrl + 'api/tank/'+tankId+'/fish')
      .map((response: Response) => response)
      .catch(this.errorHandler);
  }
  getFishById(id: number) {
    return this._http.get(this.myAppUrl + "api/fish/" + id)
      .map((response: Response) => response)
      .catch(this.errorHandler)
  }
  saveFish(fish) {
    return this._http.post(this.myAppUrl + 'api/fish', fish)
      .map((response: Response) => response)
      .catch(this.errorHandler)
  }

  deleteFish(id) {
    return this._http.delete(this.myAppUrl + "api/fish/" + id)
      .map((response: Response) => response)
      .catch(this.errorHandler);
  }
  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
