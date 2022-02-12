const videoElement = document.getElementsByClassName('input_video')[0]; //Getting the camera input from index file
let landmarks

const pose = new Pose({locateFile: (file) => {
  return `https://cdn.jsdelivr.net/npm/@mediapipe/pose/${file}`;//Loading the BlazePose model 
}});

pose.setOptions({
  modelComplexity: 1,//setting the BlazePose model to lite
  smoothLandmarks: true,
  enableSegmentation: true,
  smoothSegmentation: true,
  minDetectionConfidence: 0.5,
  minTrackingConfidence: 0.5
});

pose.onResults(results => {
  landmarks = results.poseLandmarks
});

const camera = new Camera(videoElement, {//setting up the camera
  onFrame: async () => {
    await pose.send({image: videoElement});
  },
  width: 1920,//width of camera input
  height: 1080//height of camera input

  // can make it repsonsive if needed
});

camera.start();//starting the camera


let one = {handposeValues: {one :{x:0,y:0,z:0}}};
// setInterval(function(){ 
//   if (landmarks && landmarks.length) {
//     for (let i = 0; i < landmarks.length; i++) {
//       one.handposeValues["data"+i] = {x:30*landmarks[i].x, y:16.875*landmarks[i].y, z:10*landmarks[i].z, w:landmarks[i].visibility}
//       GlobalUnityInstance.SendMessage("DataReceiver","keypointData", JSON.stringify(one.handposeValues));
//     }
//     var d = dist(landmarks[2].x, landmarks[2].y, landmarks[5].x, landmarks[5].y);
//     var distance = {scale:d*20};
//     GlobalUnityInstance.SendMessage("DataReceiver","dist", JSON.stringify(distance));
//   }
// }, 100);
function draw(){
  if (landmarks && landmarks.length) {
    for (let i = 0; i < landmarks.length; i++) {
      one.handposeValues["data"+i] = {x:30*landmarks[i].x, y:16.875*landmarks[i].y, z:15*landmarks[i].z, w:landmarks[i].visibility}
      GlobalUnityInstance.SendMessage("DataReceiver","keypointData", JSON.stringify(one.handposeValues));
    }
    var d = dist(landmarks[2].x, landmarks[2].y, landmarks[5].x, landmarks[5].y);
    var distance = {scale:d*20};
    GlobalUnityInstance.SendMessage("DataReceiver","dist", JSON.stringify(distance));
  }
}


    // var lShoulx = 10*landmarks[11].x;
    // var lShouly = 10*landmarks[11].y;
    // var lShoul = {x:lShoulx, y:lShouly, z:1*landmarks[11].z};
    // GlobalUnityInstance.SendMessage("DataReciiver","keypointData11", JSON.stringify(lShoul));