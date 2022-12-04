import { Component, OnInit } from '@angular/core';
import { NgImageSliderModule } from 'ng-image-slider';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void { }

  imgCollection: Array<object> = [
    {
      image: '../../assets/homePictures/image1.jpg',
      thumbImage: '../../assets/homePictures/image1.jpg',
      alt: 'Image 1',
      title: 'Image 1'
    }, {
      image: '../../assets/homePictures/image2.jpg',
      thumbImage: '../../assets/homePictures/image2.jpg',
      title: 'Image 2',
      alt: 'Image 2'
    }, {
      image: '../../assets/homePictures/image3.jpg',
      thumbImage: '../../assets/homePictures/image3.jpg',
      title: 'Image 3',
      alt: 'Image 3'
    }, {
      image: '../../assets/homePictures/image4.jpg',
      thumbImage: '../../assets/homePictures/image4.jpg',
      title: 'Image 4',
      alt: 'Image 4'
    }, {
      image: '../../assets/homePictures/image5.jpg',
      thumbImage: '../../assets/homePictures/image5.jpg',
      title: 'Image 5',
      alt: 'Image 5'
    },
    {
      image: '../../assets/homePictures/image6.jpg',
      thumbImage: '../../assets/homePictures/image6.jpg',
      title: 'Image 6',
      alt: 'Image 6'
    },
    {
      image: '../../assets/homePictures/image7.jpg',
      thumbImage: '../../assets/homePictures/image7.jpg',
      title: 'Image 7',
      alt: 'Image 7'
    },
    {
      image: '../../assets/homePictures/image8.jpg',
      thumbImage: '../../assets/homePictures/image8.jpg',
      title: 'Image 8',
      alt: 'Image 8'
    }
  ];

}
