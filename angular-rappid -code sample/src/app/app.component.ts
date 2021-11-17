import { OnInit, Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { dia, ui, shapes, util } from '@clientio/rappid';
import { link } from 'fs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {

  @ViewChild('canvasHTML', { read: '', static: true }) canvas: ElementRef;

  private graph: dia.Graph;
  private paper: dia.Paper;
  private scroller: ui.PaperScroller;

  public ngOnInit(): void {

    const graph = this.graph = new dia.Graph({}, { cellNamespace: shapes });

    const paper = this.paper = new dia.Paper({
      model: graph,
      background: {
        color: '#F8F9FA',
      },
      frozen: true,
      async: true,
      cellViewNamespace: shapes
    });

    const scroller = this.scroller = new ui.PaperScroller({
      paper,
      autoResizePaper: true,
      cursor: 'grab'
    });

    scroller.render();

    const headeredPool = new shapes.bpmn2.HeaderedPool({
      position: { x: 25, y: 25 },
      size: { width: 900, height: 650 },
      attrs: {
        laneHeaders: {
          fill: 'lightgray',
        },
        laneLabels: {
          fill: 'gray',
          "font-size": '18'
        },
        milestoneHeaders: {
          fill: '#00fa9a',
          stroke: '#333333'
        },
        milestoneLines: {
          stroke: '#3cb371',
          strokeWidth: 1
        },
        milestoneLabels: {
          fill: '#333333',
          fontStyle: 'italic'
        },
        header: {
          fill: 'lightgray',
        },
        headerLabel: {
          text: 'Express Reporting',
          fill: 'gray'
        }
      },
      headerSize: 30,
      lanes: [
        {
          label: 'Consultant',
        },
        {
          label: 'Manager'
        },
        {
          label: 'Data Entry Clerk',
        }
      ]
    });
    var submitExpenseReport = util.breakText('Submit Expense Report', {
      width: 120,
      height: 50
    });
    const subExpRep = new shapes.standard.Rectangle({
      position: { x: 120, y: 80 },
      size: { width: 140, height: 70 },
      attrs: {
        body: { fill: '#7864f7' },
        label: {
          text: submitExpenseReport,
          fill: 'white',
          fontSize: 14,
        }
      }
    });
    var receiveExpenserReport = util.breakText('Receive Expense Report', {
      width: 120,
      height: 50
    });
    const recExpRep = subExpRep.clone();
    recExpRep.translate(0, 200);
    recExpRep.attr('label/text', receiveExpenserReport);

    const retRep = subExpRep.clone();
    retRep.translate(640, 185);
    retRep.attr('label/text', 'Return Report');

    const fwdRep = subExpRep.clone();
    fwdRep.translate(450, 290);
    fwdRep.attr('label/text', 'Forward Report');
    var enterDataSystem = util.breakText('Enter Data to System', {
      width: 120,
      height: 50
    });
    const enterData = subExpRep.clone();
    enterData.translate(450, 450);
    enterData.attr('label/text', enterDataSystem);

    var corRep = new shapes.standard.Polygon({
      position: { x: 340, y: 300 },
      size: { width: 130, height: 90 },
      attrs: {
        root: { title: 'joint.shapes.standard.Polygon' },
        label: {
          text: 'Correct Report',
        },
        body: { refPoints: '0,10 10,0 20,10 10,20' }
      }
    });

    var link1 = new shapes.standard.Link();
    link1.source(subExpRep);
    link1.target(recExpRep);

    var link2 = new shapes.standard.Link();
    link2.source(recExpRep);
    link2.target(corRep);

    var link3 = new shapes.standard.Link();
    link3.appendLabel({
      attrs: {
        text: {
          text: 'No'
        }
      }
    });
    link3.source(corRep);
    link3.target(retRep);

    var link4 = new shapes.standard.Link();
    link4.appendLabel({
      attrs: {
        text: {
          text: 'Yes'
        }
      }
    });
    link4.source(corRep);
    link4.target(fwdRep);

    var link5 = new shapes.standard.Link();
    link5.source(fwdRep);
    link5.target(enterData);

    // headeredPool.addTo(graph);
    this.graph.addCell(headeredPool);

    this.graph.addCell(subExpRep);
    this.graph.addCell(recExpRep);
    this.graph.addCell(retRep);
    this.graph.addCell(fwdRep);
    this.graph.addCell(enterData);
    this.graph.addCell(corRep);
    this.graph.addCell(link1);
    this.graph.addCell(link2);
    this.graph.addCell(link3);
    this.graph.addCell(link4);
    this.graph.addCell(link5);

  }
  public ngAfterViewInit(): void {
    const { scroller, paper, canvas } = this;
    canvas.nativeElement.appendChild(this.scroller.el);
    scroller.center();
    paper.unfreeze();
  }
}



// var subRecLink = new shapes.standard.Link({
//   attrs: {
//     line: {
//       connection: true,
//       stroke: '#333333',
//       strokeWidth: 1,
//       // strokeLinejoin: 'round',
//       // targetMarker: {
//       //   'type': 'path',
//       //   'd': 'M 10 -5 0 0 10 5 z'
//       // }
//     }
//   },
// });
// subRecLink.source(subExpRep);
// subRecLink.target(recExpRep);
// // subRecLink.vertices([
// //   { x: 100, y: 70 },
// //   { x: 100, y: 160 },
// //   { x: 170, y: 160 }
// // ]);
// // subRecLink.h
// subRecLink.addTo(graph);


// var recCorLink = new shapes.standard.Link({
//   attrs: {
//     line: {
//       connection: true,
//       stroke: '#333333',
//       strokeWidth: 1,
//       // strokeLinejoin: 'round',
//       // targetMarker: {
//       //   'type': 'path',
//       //   'd': 'M 10 -5 0 0 10 5 z'
//       // }
//     }
//   },
// });
// recCorLink.source(recExpRep);
// recCorLink.target(corRep);
// // subRecLink.vertices([
// //   { x: 100, y: 70 },
// //   { x: 100, y: 160 },
// //   { x: 170, y: 160 }
// // ]);
// // subRecLink.h
// recCorLink.addTo(graph);

// // var subRec = new shapes.standard.Link({
// //   attrs: {
// //     line: {
// //       connection: true,
// //       stroke: '#333333',
// //       strokeWidth: 1,
// //       strokeLinejoin: 'round',
// //       targetMarker: {
// //         'type': 'path',
// //         'd': 'M 10 -5 0 0 10 5 z'
// //       }
// //     }
// //   },
// //   source: ,
// //   target: 
// // });
// subRecLink.addTo(graph);