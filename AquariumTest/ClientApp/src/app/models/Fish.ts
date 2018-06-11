import { Species } from "./Species";

export class Fish  {
  public id: number;
  public speciesId: number;
  public tankId: number;
  public name: string;
  public color: string;

  public species: Species;
}
