import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FishService } from '../services/fishService.service';
import { Tank } from '../models/tank';
import { Species } from '../models/Species';
import { SpeciesService } from '../services/speciesService.service';
import { Fish } from '../models/Fish';
import { TankService } from '../services/tankService.service';
import { SpeciesPredator } from '../models/SpeciesPredator';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Rx';

@Component({
  selector: 'create-fish',
  templateUrl: './create-fish.component.html'
})

export class CreateFishComponent implements OnInit {

  fishForm: FormGroup;
  fish: Fish;
  speciesList: Array<any> = [];
  colorList: Array<any> = [];
  fishesInTank: Array<any> = [];
  predatorList: Array<any> = [];
  title: string = "Add Fish";
  tankId: number;
  errorMessage: any;

  constructor(private _fb: FormBuilder, private _route: ActivatedRoute,
    private _fishService: FishService, private _speciesService: SpeciesService,
    private _router: Router) {
    if (this._route.snapshot.params["tankId"]) {
      this.tankId = this._route.snapshot.params["tankId"];
    }

    this.fishForm = this._fb.group({
      id: 0,
      name: ['', [Validators.required]],
      species: ['', [Validators.required]],
      color: ['', [Validators.required]],
    })


  }

  ngOnInit() {
    this.getSpecies();
    this.getColors();
  }

  getSpecies() {
    this._speciesService.getSpecies().subscribe(
      data => {
        this.speciesList = data;
      }
    )
  }

  getColors() {
    this.colorList = new Array();
    this.colorList.push('Blue');
    this.colorList.push('Brown');
    this.colorList.push('Purple');
    this.colorList.push('Pink');
    this.colorList.push('Black');
    this.colorList.push('Gold');
    this.colorList.push('Gray');
    this.colorList.push('Orange');
  }

  async save() {
    if (!this.fishForm.valid) {
      return;
    }

    var add = true;

    var canAdd = await this.canbeAdded();

    if (!canAdd) {
      var res = confirm("Your tank is not compatible, your fish may die");

      if (!res)
        add = false;
    }

    if (add) {
      this.saveFish();
    }
     
  }

  saveFish() {
      this.fish = new Fish();
      this.fish.tankId = this.tankId;
      this.fish.name = this.name.value;
      this.fish.color = this.color.value;
      this.fish.speciesId = this.species.value;

      this._fishService.saveFish(this.fish)
        .subscribe((data) => {
          this.goBack();
        }, error => this.errorMessage = error)
  }

  cancel() {
    this.goBack();
  }

  goBack(): void {
    this._router.navigate(['../'], { relativeTo: this._route });
  }

  async canbeAdded(): Promise<Boolean>{

    var tankSpeciesIds: Array<number> = [];
    var tankPredatorIds: Array<number> = [];
    var fishpredatorIds: Array<number> = [];

    var tankFishes = await this._fishService.getFishes(this.tankId).toPromise();
    var currentSpecies = await this._speciesService.getSpeciesById(this.species.value).toPromise();

    await Promise.all(tankFishes.map( async(element) => {
      var fish = element as Fish;

      if (!tankSpeciesIds.includes(fish.speciesId))
      {
        tankSpeciesIds.push(fish.speciesId);

        var tankSpecies = await this._speciesService.getSpeciesById(fish.speciesId).toPromise() as Species;
       
        await tankSpecies.predators.map(x=> {
          var sp = x as SpeciesPredator;
          tankPredatorIds.push(sp.predatorId);
        })
      }
    }));

    currentSpecies.predators.forEach(pred => {
      fishpredatorIds.push(pred.predatorId);
    });

    var tankHasPred = fishpredatorIds.filter(function (n) {
      return tankSpeciesIds.indexOf(n) > -1;
    }).length > 0;

    var tankHasPreys = tankPredatorIds.includes(currentSpecies.id);
    return !tankHasPred && !tankHasPreys;
  }

  get name() { return this.fishForm.get('name'); }
  get species() { return this.fishForm.get('species'); }
  get color() { return this.fishForm.get('color'); }
}  
