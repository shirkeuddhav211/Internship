import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { GoogleMap } from '@angular/google-maps';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Inspection } from '../inspection/inspections-list/inspectionsmodel';
import { InspectionService } from '../inspection/inspection.service';
import { ToastrService } from 'ngx-toastr';

interface MarkerProperties {
  position: {
    lat: number;
    lng: number;
  }
};

@Component({
  selector: 'app-inspection-map',

  templateUrl: './inspection-map.component.html',
  styleUrl: './inspection-map.component.scss'
})

export class InspectionMapComponent {
  @ViewChild('myGoogleMap', { static: true }) map!: GoogleMap;
  mapOptions: google.maps.MapOptions = {
    center: { lat: 38.581183489943626, lng: -121.50410089628544 },
    zoom: 13,
  };
  //markers: MarkerProperties[] = []
  markers: { position: google.maps.LatLngLiteral }[] = [];
  addressToPlot = 'Chiplun'; // Example address
  // markers: MarkerProperties[] = [
  //   { position: { lat:  38.56077260749548, lng: -121.47706439946126 }}, // Eiffel Tower
  //   { position: { lat: 38.569310825359295, lng: -121.40032667676682 }}, // Louvre Museum
  //   { position: { lat: 38.650500207858826, lng:-121.51121710375098 }}, // Cathédrale Notre-Dame de Paris
  // ];

  inspectionList:Inspection[];

  constructor(private messageService: MessageService,
    private route: Router, private http: HttpClient, private changeDetector: ChangeDetectorRef,
    public inspectionTypeService : InspectionService,public toastr:ToastrService,
  ) { }



  ngOnInit() {
    console.log('Markers:', this.markers);
   // this.geocodeAddress(this.addressToPlot);
    this.GetInspectionListWithoutDate()
  }

  geocodeAddress(address: string) {
    // Geocode the address to get its coordinates
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, (results, status) => {
      if (status === 'OK') {
        // Place a marker on the map at the coordinates of the address
        let location = (results[0].geometry.location);
        this.markers.push({ position: { lat: location.lat(), lng: location.lng() } });
        //this.markers.push({ position: location});
        // const marker = new AdvancedMarkerElement({
        //   position: location,
        //   map: this.map
        // });
        console.log('Markers:', this.markers);
        // Trigger change detection manually
        this.changeDetector.detectChanges();
      } else {
        console.error('Geocode was not successful for the following reason: ' + status);
      }
    });
  }

  public GetInspectionListWithoutDate() {    
    this.inspectionList=[];
    this.inspectionTypeService.GetInspectionListWithoutDate().subscribe((response: Inspection[]) => {
     // this.inspectionList = response
      this.inspectionList = response.filter(x=>x.AddressLine1)
      this.inspectionList.forEach(element => {
        this.geocodeAddress(element.AddressLine1)
        
    })
      
    },
    (error:any)=> {
      this.toastr.error('Error while fetching Inspection Type', 'Error');
      //this.spinner.hide();
    });
  }
  
  GotoInsp() {
    this.route.navigate(["/app/inspectionformlist"]);
  }


  //#region working
  // addmarker(event:google.maps.MapMouseEvent){
  //   if(event.latLng != null){
  //     this.markerPositions.push(event.latLng.toJSON())
  //   }

  // }

  //#endregion
  // mapOptions: google.maps.MapOptions = {
  //   center: { lat: 38.9987208, lng: -77.2538699 },
  //   zoom : 14
  // }
  // marker = {
  //   position: { lat: 38.9987208, lng: -77.2538699 },
  // }
  //////////////////new 




}
