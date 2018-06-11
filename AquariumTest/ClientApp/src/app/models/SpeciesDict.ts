import { Species } from "./Species";

export class SpeciesDict{
  public species: Species;
  public predators: Species[];

  constructor() {
    this.predators = new Array();
  }
}
