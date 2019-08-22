(function(window, document, undefined) {

  if(!window.LayoutApp) {
    window.LayoutApp = {};
  }

  window.LayoutApp.Custom = (function() {

    var CUSTOM_LAYOUTS = [
      /**
      * Place your custom layouts as below
      */
      {
        minImages: 3,
        imageData: [
          {
            widthPercent: 60,
            heightPercent: 100,
            targetX: 0,
            targetY: 0
          },
          {
            widthPercent: 20,
            heightPercent: 100,
            targetX: 120,
            targetY: 0
          },
          {
            widthPercent: 20,
            heightPercent: 100,
            targetX: 160,
            targetY: 0
          },
        ],
        setImageMeasures: function (layout, targetCanvas, imageIndex) {
          var imageData = this.imageData[imageIndex];
          if( imageData) {
              return {
                width: imageData.widthPercent * $(targetCanvas).width() / 100,
                height: imageData.heightPercent * $(targetCanvas).height() / 100
              };
          }

          return {
            height: 0,
            width: 0
          }
        },
        setSourceCoordinates: function (canvas, layout, imageWidth, imageHeight, imageIndex) {
          return {
            x: 50,
            y: 0
          }
        },
        setTargetCoordinates: function (targetCanvas, layout, imageWidth, imageHeight, imageIndex) {
          var imageData = this.imageData[imageIndex];

          if (imageData) {
            return {
              x: imageData.targetX,
              y: imageData.targetY
            }
          }

          return {
            x: 0,
            y: 0
          }
        }
      }
    ];

    return {
      getCustomLayouts: function() {
        return CUSTOM_LAYOUTS;
      }
    }

  })();

})(window, document);
