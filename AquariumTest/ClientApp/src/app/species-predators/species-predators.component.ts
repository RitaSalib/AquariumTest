import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { TankService } from '../services/tankService.service';
import { Tank } from '../models/tank';
import { SpeciesService } from '../services/speciesService.service';
import { Species} from '../models/Species';
import { $ } from 'protractor';
import { SpeciesDict } from '../models/SpeciesDict';


@Component({
  selector: 'species-predators',
  templateUrl: './species-predators.component.html'
})
export class SpeciesPredatorsComponent {
  public species: Species[];
  public speciesDict: SpeciesDict[];
  constructor(public http: HttpClient, private _router: Router, private _speciesService: SpeciesService) {
    this.getSpecies();
    this.speciesDict = new Array();
  }
  getSpecies() {
    this._speciesService.getSpecies().subscribe(
      data => {
        this.species = data;
        for (let sp of this.species) {
          this._speciesService.getSpeciesById(sp.id).subscribe(
            x => {
              this.speciesDict.push(this.getPredators(x));
            }
          )
          }
        }
    )
  }

  getPredators(x: Species): SpeciesDict{
    var spdict = new SpeciesDict();
    spdict.species = x;
    x.predators.forEach(element => {
      this._speciesService.getSpeciesById(element.predatorId).subscribe(
        y => {
          spdict.predators.push(y);
        }
      )
    });

    return spdict;
  }


}


