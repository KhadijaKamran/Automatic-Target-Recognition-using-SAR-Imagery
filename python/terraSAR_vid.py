import numpy as np
import cv2
import os
import matplotlib.pyplot as plt
import sys

# display function to show image on Jupyter
def display_img(img,cmap=None):
    fig = plt.figure(figsize = (12,12))
    plt.axis(False)
    ax = fig.add_subplot(111)
    ax.imshow(img,cmap)

labelsPath = os.path.join("E:\\FYP\\YOLO\\darknet\\data\\yolo.names")
LABELS = open(labelsPath).read().strip().split("\n")
#print(LABELS)
# derive the paths to the YOLO weights and model configuration
weightsPath = os.path.join("E:\\FYP\\YOLO\\darknet\\backup\\yolov3_custom_train_final.weights")
configPath = os.path.join("E:\\FYP\\YOLO\\darknet\\cfg\\yolov3_custom_train.cfg")

# Loading the neural network framework Darknet (YOLO was created based on this framework)
net = cv2.dnn.readNetFromDarknet(configPath,weightsPath)

# Create the function which predict the frame input
def predict(image):
    
    # initialize a list of colors to represent each possible class label
    np.random.seed(42)
    COLORS = np.random.randint(0, 255, size=(len(LABELS), 3), dtype="uint8")
    (H, W) = image.shape[:2]
    #print(H,W)
    # determine only the "ouput" layers name which we need from YOLO
    ln = net.getLayerNames()
    #print(len(ln),ln)
    ln = [ln[i[0] - 1] for i in net.getUnconnectedOutLayers()]
    #print(len(ln),ln)
    # construct a blob from the input image and then perform a forward pass of the YOLO object detector, 
    # giving us our bounding boxes and associated probabilities
    blob = cv2.dnn.blobFromImage(image, 1 / 255.0, (416, 416), swapRB=True, crop=False)
    net.setInput(blob)
    layerOutputs = net.forward(ln)
    
    boxes = []
    confidences = []
    obj_confidences = []
    classIDs = []
    threshold = 0.2
    
    # loop over each of the layer outputs
    for output in layerOutputs:
        # loop over each of the detections
        for detection in output:
            # extract the class ID and confidence (i.e., probability) of
            # the current object detection
            
            scores = detection[5:]
            object_conf = detection[4]
            classID = np.argmax(scores)
            confidence = scores[classID]

            # filter out weak predictions by ensuring the detected
            # probability is greater than the minimum probability
            # confidence type=float, default=0.5
            if confidence > threshold:
                # scale the bounding box coordinates back relative to the
                # size of the image, keeping in mind that YOLO actually
                # returns the center (x, y)-coordinates of the bounding
                # box followed by the boxes' width and height
                box = detection[0:4] * np.array([W, H, W, H])
                (centerX, centerY, width, height) = box.astype("int")

                # use the center (x, y)-coordinates to derive the top and
                # and left corner of the bounding box
                x = int(centerX - (width / 2))
                y = int(centerY - (height / 2))

                # update our list of bounding box coordinates, confidences,
                # and class IDs
                boxes.append([x, y, int(width), int(height)])
                confidences.append(float(confidence))
                obj_confidences.append(float(object_conf))
                classIDs.append(classID)

    # apply non-maxima suppression to suppress weak, overlapping bounding boxes
    idxs = cv2.dnn.NMSBoxes(boxes, confidences, threshold, 0.1)
    #print(boxes, confidences, threshold)
    #print(idxs)
    # ensure at least one detection exists
    result = []
    if len(idxs) > 0:
        # loop over the indexes we are keeping
        for i in idxs.flatten():
            # extract the bounding box coordinates
            (x, y) = (boxes[i][0], boxes[i][1])
            (w, h) = (boxes[i][2], boxes[i][3])
            result.append([x,y,w,h,obj_confidences[i],confidences[i],classIDs[i]])
            # draw a bounding box rectangle and label on the image
            color = (255,0,0)
            cv2.rectangle(image, (x, y), (x + w, y + h), color, 2)
            #print(x,y,w,h)
            text = "{}".format(LABELS[classIDs[i]], obj_confidences[i])
            text2 = str(obj_confidences[i])[:6]
            #print(text)
            cv2.putText(image, text, (x +15, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, color, 2)
            cv2.putText(image, text2, (x +15, y + 20), cv2.FONT_HERSHEY_SIMPLEX,0.5, color, 2)
    return image, result

cap =cv2.VideoCapture(sys.argv[1])
number_frame = 20.0 #higher frames better quality of the video
video_size = (491,594)
#video_size = (492,594)
fourcc = cv2.VideoWriter_fourcc(*'DIVX')
#out = cv2.VideoWriter('/content/drive/My Drive/darknet/data/videos/0_detection.mp4',fourcc, number_frame,video_size)
out = cv2.VideoWriter(sys.argv[1][:-4]+"_detected.mp4",fourcc, number_frame,video_size)
while True:
    ret,frame = cap.read() 
    if ret:
        frame,result = predict(frame)
        out.write(frame)

        if cv2.waitKey(1) & 0xff == ord("q"):
            break
    else:
        break

cap.release()   
out.release()
cv2.destroyAllWindows()
