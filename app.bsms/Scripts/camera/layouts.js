(function(window, document, jQuery, Custom, undefined) {

  if(!window.LayoutApp) {
    window.LayoutApp = {};
  }

  window.LayoutApp.Layouts = (function() {

    var CANVAS_MAX_MEASURE = 200,
        LAYOUT_TYPES = {
          HORIZONTAL: 'horizontal',
          VERTICAL: 'vertical'
        },
        LAYOUTS = [
          {
            type: LAYOUT_TYPES.VERTICAL
          },
          {
            type: LAYOUT_TYPES.HORIZONTAL
          }
        ];

    return {

      getCanvasMaxWidth: function() {
        return CANVAS_MAX_MEASURE;
      },

      getLayouts: function() {
        return LAYOUTS.concat(Custom.getCustomLayouts());
      },

      isHorizontal: function(layout) {
        return layout.type === LAYOUT_TYPES.HORIZONTAL;
      },

      isVertical: function(layout) {
        return layout.type === LAYOUT_TYPES.VERTICAL;
      },

      isAvailable: function(layout, totalImages) {
        return !layout.minImages || layout.minImages <= totalImages;
      }

    }

  })();

})(window, document, jQuery, window.LayoutApp.Custom);
