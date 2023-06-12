import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { AppReport } from '../model/AppReport';
import { WorkItem } from '../model/WorkItem';
import { WorkItemType } from '../model/WorkItemType';

@Component({
  selector: 'app-appointment-report',
  templateUrl: './appointment-report.component.html',
  styleUrls: ['./appointment-report.component.css']
})
export class AppointmentReportComponent {
  workItemTypes: WorkItemType[] = Object.values(WorkItemType);
  report!: AppReport;
  workitem!: WorkItem;
  lastId: any;

  checkedTypes: { [key in WorkItemType]: boolean } = {
    [WorkItemType.Needle]: false,
    [WorkItemType.Syringe]: false,
    [WorkItemType.TransfusionBag]: false,
    [WorkItemType.TransfusionPipe]: false,
    [WorkItemType.Gloves]: false,
    [WorkItemType.TestTube]: false
  };

  usedInstances: { [key in WorkItemType]: number } = {
    [WorkItemType.Needle]: 0,
    [WorkItemType.Syringe]: 0,
    [WorkItemType.TransfusionBag]: 0,
    [WorkItemType.TransfusionPipe]: 0,
    [WorkItemType.Gloves]: 0,
    [WorkItemType.TestTube]: 0
  };

  ngOnInit() {
    this.lastId = this.route.snapshot.paramMap.get('id');
  }

  constructor(private clinicService: ClinicService, private route: ActivatedRoute) {
    this.report = new AppReport({});
  }

  updateCheckedTypes(type: WorkItemType, event: Event) {
    const checkbox = event.target as HTMLInputElement;
    this.checkedTypes[type] = checkbox.checked;
  }

  updateUsedInstances(type: WorkItemType, value: string) {
    this.usedInstances[type] = parseInt(value, 10);
  }

  saveValues() {
    console.log(this.checkedTypes);
    console.log(this.usedInstances);
    console.log(this.report.description);
    console.log(this.lastId);
    this.report.id = this.lastId;
    const enumValuesAsNumbers = this.workItemTypes
      .filter(type => this.checkedTypes[type] && this.usedInstances[type] !== 0)
      .map(type => ({
        workItemType: WorkItemType[type], 
        usedInstances: this.usedInstances[type]
      }))
      .map(item => ({
        workItemType: this.getEnumNumberValue(item.workItemType),
        usedInstances: item.usedInstances
      }));

    const payload = {
      report: {
        description: this.report.description,
        equipment: enumValuesAsNumbers
      },
      appointmentId: this.lastId
    };

    this.clinicService.saveApp(payload).subscribe(
      res => {

        console.log("Report saved successfully:", res);
        // Handle the response as needed
      },
      error => {
        console.error("Error saving report:", error);
        // Handle the error as needed
      }
    );
  }

  getEnumNumberValue(enumValue: string): number {
    // Perform the conversion from string value to numeric value
    switch (enumValue) {
      case WorkItemType.Needle: return 0;
      case WorkItemType.Syringe: return 1;
      case WorkItemType.TransfusionBag: return 2;
      case WorkItemType.TransfusionPipe: return 3;
      case WorkItemType.Gloves: return 4;
      case WorkItemType.TestTube: return 5;
      default: return -1; // Handle any other cases or errors
    }

  }
}
