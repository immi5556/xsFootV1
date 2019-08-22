(function (window, document, $, Layouts, undefined) {

  window.LayoutApp = (function () {

    var CANVAS = 'canvas',
        MAX_MEASURE = Layouts.getCanvasMaxWidth(),

        images = [],
        canvases = [],
        canvasesSelected = [],
        measuresSet = false,
        measures = {
          width: MAX_MEASURE
        },

        layoutOptions = $('#layout-options'),
        gallery = $('#gallery'),
        camera,

        layouts = Layouts.getLayouts(),

        /**
         * It was not worth it having a new module just because of this function,
         * but still sounds so weird having this here.
         */
        isFunction = function(f) {
          return typeof f === 'function';
        },

        initCamera = function () {
          if (!window.JpegCamera) {
            alert('Camera access is not available in your browser');
          } else {
              var options = {
                  shutter_ogg_url: "~/scripts/jpeg_camera/jpeg_camerashutter.ogg",
                  shutter_mp3_url: "~/scripts/jpeg_camera/jpeg_camerashutter.mp3",
                  swf_url: "~/scripts/jpeg_camera/jpeg_camera.swf"
              }

              camera = new JpegCamera('#camera', options)
              .ready(function (resoltution) {
                /**
                 * Something you'd need to do if the camera is ready.
                 */
              })
              .error(function () {
              alert('Camera access was denied');
            });
          }
        },

        updateGallery = function (canvas) {
          if (images.length === 1) {
            gallery.html('');
          }

          gallery.append(canvas);
        },

        setImageMeasures = function (layout, targetCanvas, imageIndex) {
          if (isFunction(layout.setImageMeasures)) {
            return layout.setImageMeasures(layout, targetCanvas, imageIndex);
          } else {
            if (Layouts.isVertical(layout)) {
              return {
                width: targetCanvas.width,
                height: targetCanvas.height / images.length
              };
            } else if (Layouts.isHorizontal(layout)) {
              return {
                width: targetCanvas.width / images.length,
                height: targetCanvas.height
              };
            }

            return {
              width: targetCanvas.width,
              height: targetCanvas.height
            };
          }
        },

        setSourceCoordinates = function (canvas, layout, imageWidth, imageHeight, imageIndex) {
          if (isFunction(layout.setSourceCoordinates)) {
            return layout.setSourceCoordinates(canvas, layout, imageWidth, imageHeight, imageIndex);
          } else {
            if (Layouts.isVertical(layout)) {
              return {
                x: 0,
                y: canvas.height / 2 - imageHeight / 2
              };
            } else if (Layouts.isHorizontal(layout)) {
              return {
                x: canvas.width / 2 - imageWidth / 2,
                y: 0
              };
            }
          }
        },

        setTargetCoordinates = function (targetCanvas, layout, imageWidth, imageHeight, imageIndex) {
          if (isFunction(layout.setTargetCoordinates)) {
            return layout.setTargetCoordinates(targetCanvas, layout, imageWidth, imageHeight, imageIndex);
          } else {
            if (Layouts.isVertical(layout)) {
              return {
                x: 0,
                y: imageHeight * imageIndex
              };
            } else if (Layouts.isHorizontal(layout)) {
              return {
                x: imageWidth * imageIndex,
                y: 0
              };
            }
          }
        },

        /**
        * To ensure the correct ratio when drawing in the new canvas.
        */
        calculateCoeficient = function (targetCanvas, sourceCanvas) {
          return {
            width: sourceCanvas.width / targetCanvas.width,
            height: sourceCanvas.height / targetCanvas.height
          }
        },


        /**
         * Fix to the intrinsic width as per:
         * http://stackoverflow.com/questions/3186150/image-draws-stretched-to-html-canvas-when-created-using-jquery
         */
        setUpCanvas = function () {
          var elem = $('<canvas>', {
                width: measures.width,
                height: measures.height
              }).addClass('selected'),
              targetCanvas = elem[0];

          targetCanvas.width = measures.width;
          targetCanvas.height = measures.height;
          return targetCanvas;
        },

        updateLayouts = function (sourceCanvas) {
          layoutOptions.html('');

          for(var i = 0, layout; layout = layouts[i]; i++) {
            if (Layouts.isAvailable(layout, canvases.length)) {
              var targetCanvas = setUpCanvas(),
                  context = targetCanvas.getContext('2d');

              for(var j = 0, canvas; canvas = canvases[j]; j++) {
                var imageMeasure = setImageMeasures(layout, targetCanvas, j),
                sourceCoordinates = setSourceCoordinates(targetCanvas, layout, imageMeasure.width, imageMeasure.height, j),
                targetCoordinates = setTargetCoordinates(targetCanvas, layout, imageMeasure.width, imageMeasure.height, j),
                coeficient = calculateCoeficient(targetCanvas, canvas);

                context.drawImage(canvas, sourceCoordinates.x, sourceCoordinates.y, imageMeasure.width * coeficient.width, imageMeasure.height * coeficient.height, targetCoordinates.x, targetCoordinates.y, imageMeasure.width, imageMeasure.height);
              }

              layoutOptions.append(targetCanvas);
            }
          }
        },

        /**
         * Maintains the measure proportion between the canvas retrieved by JPEG Camera
         * and the one to be generated with the layouts
         */
        setCanvasMeasures = function (canvas) {
          measures.height = canvas.height * MAX_MEASURE / canvas.width;
        },

        updateView = function (canvas) {
          canvas.selected = true;
          canvases.push(canvas);

          if (!measuresSet) {
            setCanvasMeasures(canvas);
            measuresSet = true;
          }

          updateGallery(canvas);
          updateLayouts(canvas);
        },

        capture = function () {
          var snapshot = camera.capture();
          images.push(snapshot);
          snapshot.get_canvas(updateView);
        },

        download = function (e) {
          var canvas = e.target;
          window.open(canvas.toDataURL().replace("image/png", "image/octet-stream"));
        },

        toggleSelection = function (e) {
          var canvasClicked = $(e.target);
          canvasClicked.toggleClass('selected');
          updateLayouts();
        },

        bindEvents = function () {
          $('#camera-wrapper').on('click', '#shoot', capture);
          $('#layout-options').on('click', 'canvas', download);
        };
        
        stopCamera = function(){
            camera.stop();
        };

    return {
        init: function () {
            initCamera();
            bindEvents();
        },
        close: function(){
            stopCamera();
        }
    }

  })();

})(window, document, jQuery, window.LayoutApp.Layouts);
