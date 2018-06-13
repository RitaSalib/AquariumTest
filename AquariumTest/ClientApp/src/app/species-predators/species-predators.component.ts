import { Component, Inject, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { TankService } from '../services/tankService.service';
import { Tank } from '../models/tank';
import { SpeciesService } from '../services/speciesService.service';
import { Species} from '../models/Species';
import { SpeciesDict } from '../models/SpeciesDict';


@Component({
  selector: 'species-predators',
  templateUrl: './species-predators.component.html'
})
export class SpeciesPredatorsComponent implements OnInit {
  public species: Species[];
  public speciesDict: SpeciesDict[];
  constructor(public http: HttpClient, private _router: Router, private _speciesService: SpeciesService) {
    this.speciesDict = new Array();
  }

  ngOnInit() {
    this.getSpecies();
  }

  async getSpecies() {
    var species = await this._speciesService.getSpecies().toPromise();
    await Promise.all(species.map(async(x) => {
      var sp = await this._speciesService.getSpeciesById(x.id).toPromise();
      this.speciesDict.push(this.getPredators(sp))
    }))

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


