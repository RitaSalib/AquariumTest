import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Tank } from '../models/tank';
import { Fish } from '../models/Fish';
import { FishService } from '../services/fishService.service';

@Component({
  selector: 'fish',
  templateUrl: './fish.component.html'
})
export class FishComponent {
  public fishes: Fish[];
  id: number;

  constructor(public http: HttpClient, private _route: ActivatedRoute, private _router: Router, private _fishService: FishService) {
    if (this._route.snapshot.params["tankId"]) {
      this.id = this._route.snapshot.params["tankId"];
    }  
    this.getFishes(this.id);
  }

  getFishes(id) {
    this._fishService.getFishes(id).subscribe(
      data => {
        this.fishes = data;
      }
    )
  }
  createFish() {
    this._router.navigate(['create-fish'], { relativeTo: this._route });
  }

  viewFish(fishId) {
    this._router.navigate(['fish-detail', fishId], { relativeTo: this._route });
  }
  deleteFish(fishId) {
    var ans = confirm("Do you want to delete this fish?");
    if (ans) {
      this._fishService.deleteFish(fishId).subscribe((data) => {
        this.getFishes(this.id);
      }, error => console.error(error))
    }
  }
}

