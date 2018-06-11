import { Component, OnInit, Input, Inject} from '@angular/core';
import { Fish } from '../models/Fish';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { FishService } from '../services/fishService.service';
import { SpeciesService } from '../services/speciesService.service';
import { Species } from '../models/Species';
import { IPromise } from 'q';

@Component({
  selector: 'fish-detail',
  templateUrl: './fish-detail.component.html'
})
export class FishDetailComponent implements OnInit {

  public selectedFish: Fish;
  public selectedSpecies: Species;
  id: number;

  constructor(public http: HttpClient, private _route: ActivatedRoute, private _router: Router,
    private _fishService: FishService, private _speciesService: SpeciesService) {
    if (this._route.snapshot.params["fishId"]) {
      this.id = this._route.snapshot.params["fishId"];
    }
  }

  getFish(id) {
    this._fishService.getFishById(id).subscribe(
      data => {
        this.selectedFish = data;
      }
    )
  }

  getSpecies(){
    this._speciesService.getSpeciesById(this.selectedFish.speciesId).subscribe(
      data => {
        this.selectedSpecies = data;
      }
    )
  }

  ngOnInit() {
    this.getFish(this.id);
  }

}
