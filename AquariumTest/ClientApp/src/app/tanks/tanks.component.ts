import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { TankService } from '../services/tankService.service';
import { Tank } from '../models/tank';


@Component({
  selector: 'tanks',
  templateUrl: './tanks.component.html'
})
export class TanksComponent {
  public tanks: Tank[];
  constructor(public http: HttpClient, private _router: Router, private _tankService: TankService) {
    this.getTanks();
  }
  getTanks() {
    this._tankService.getTanks().subscribe(
      data => this.tanks = data
    )
  }

  viewFish(tankId) {
    this._router.navigate(['/fish', tankId]);  
  }
}

