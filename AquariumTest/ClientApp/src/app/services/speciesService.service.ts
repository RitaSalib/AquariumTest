import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
@Injectable()
export class SpeciesService {
  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }
  getSpecies() {
    return this._http.get(this.myAppUrl + 'api/species/')
      .map((response: Response) => response)
      .catch(this.errorHandler);
  }
  getSpeciesById(id: number) {
    return this._http.get(this.myAppUrl + "api/species/" + id)
      .map((response: Response) => response)
      .catch(this.errorHandler)
  }
  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
