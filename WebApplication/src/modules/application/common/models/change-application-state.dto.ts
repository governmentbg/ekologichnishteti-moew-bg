export class ChangeApplicationStateDto {
  parentRouteId: number;
  changeStateDescription: string;

  constructor(parentRouteId: number, changeStateDescription: string) {
    this.parentRouteId = parentRouteId;
    this.changeStateDescription = changeStateDescription;
  };
}