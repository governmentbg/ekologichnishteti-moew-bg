import { Injectable } from "@angular/core";
import { ActivityOfferingDropdownDto } from "../../nomenclature/dtos/activity-offering-dropdown.dto";

@Injectable({
  providedIn: 'root'
})
export class ActivityOfferingService {

  getActivityOfferingDropdownDto(activityOfferings: any[]): any {
    let result = [];
    activityOfferings.forEach(activityOffering => {
      let activityOfferingDropdownDto = new ActivityOfferingDropdownDto();
      activityOfferingDropdownDto = JSON.parse(JSON.stringify(activityOffering));
      activityOfferingDropdownDto.name = `${activityOffering.subActivity.code} ${activityOffering.activity.name}`;

      result.push(activityOfferingDropdownDto);
    });

    return result;
  }

}