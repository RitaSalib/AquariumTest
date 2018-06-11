import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FishService } from '../services/fishService.service';
import { Tank } from '../models/tank';
import { Species } from '../models/Species';
import { SpeciesService } from '../services/speciesService.service';
import { Fish } from '../models/Fish';
import { forEach } from '@angular/router/src/utils/collection';

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
  title: string = "Create";
  tankId: number;
  errorMessage: any;

  constructor(private _fb: FormBuilder, private _route: ActivatedRoute,
    private _fishService: FishService, private _speciesService: SpeciesService, private _router: Router) {
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

  save() {
    if (!this.fishForm.valid) {
      return;
    }


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

  get name() { return this.fishForm.get('name'); }
  get species() { return this.fishForm.get('species'); }
  get color() { return this.fishForm.get('color'); }
}  
