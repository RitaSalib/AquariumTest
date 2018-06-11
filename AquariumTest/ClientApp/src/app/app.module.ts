import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TanksComponent } from './tanks/tanks.component';
import { TankService } from './services/tankService.service';
import { FishService } from './services/fishService.service';
import { FishComponent } from './fish/fish.component';
import { FishDetailComponent } from './fish-detail/fish-detail.component';
import { CreateFishComponent } from './create-fish/create-fish.component';
import { SpeciesService } from './services/speciesService.service';
import { CommonModule } from '@angular/common';
import { SpeciesPredatorsComponent } from './species-predators/species-predators.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FishComponent,
    TanksComponent,
    FishDetailComponent,
    CreateFishComponent,
    SpeciesPredatorsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'tanks', pathMatch: 'full' },
      {
        path: 'species-predators',
        component: SpeciesPredatorsComponent
      },
      {
        path: 'tanks',
        component: TanksComponent
      },
      {
        path: 'tanks/:tankId',
        component: FishComponent,
      },
      { path: 'tanks/:tankId/fish-detail/:fishId', component: FishDetailComponent },
      { path: 'tanks/:tankId/create-fish', component: CreateFishComponent },
     { path: '**', redirectTo: 'tanks' }
    ])
  ],
  providers: [TankService, FishService, SpeciesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
