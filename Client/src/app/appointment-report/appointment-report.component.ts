import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClinicService } from '../../service/clinic.service';
import { AppReport } from '../model/AppReport';
import { User } from '../model/User';
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
  isSaving: boolean = false;
  successMessage: string = '';
  appotinmentId!: any;
  user!: User;


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

  constructor(private clinicService: ClinicService, private route: ActivatedRoute, private router: Router) {
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
        name: WorkItemType[type],
        usedInstances: this.usedInstances[type]
      }))
      .map(item => ({
        name: item.name,
        usedInstances: item.usedInstances
      }));

    const payload = {
      report: {
        description: this.report.description,
        equipment: enumValuesAsNumbers
      },
      appointmentId: this.lastId
    };
    this.isSaving = true;
    this.successMessage = '';
    this.clinicService.saveApp(payload).subscribe(
      res => {
        console.log("Report saved successfully:", res);

        this.successMessage = 'Report saved successfully!';

        // Re-enable input fields
        this.isSaving = false;
      },
      error => {
        console.error("Error saving report:", error);
        // Handle the error as needed
      }
    );
  }
  finishApp() {
    this.appotinmentId = this.route.snapshot.paramMap.get('id');
    console.log(this.appotinmentId);
    /*this.clinicService.finishAppointment(this.appotinmentId).subscribe(res => {
      
    });*/
  }
}
