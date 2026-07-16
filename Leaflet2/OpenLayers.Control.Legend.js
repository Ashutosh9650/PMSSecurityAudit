OpenLayers.Control.Legend =
  OpenLayers.Class(OpenLayers.Control, {

    /**
     * in der Eigenschaft div wird eine
     * Referenz auf das <div> gespeichert.
     */
    div: null,

    draw: function() {
      // wird initial aufgerufen und
      // delegiert die eigentliche Arbeit an redraw
      this.redraw();
    },

    clearLegendDiv: function(){
      // Legendenelemente entfernen:
      if ( this.div.hasChildNodes() ) {
        while ( this.div.childNodes.length >= 1 ) {
          this.div.removeChild( this.div.firstChild );
        }
      }
    },

    redraw: function() {
      // Nur wenn die Control aktiv ist, soll neugezeichnet werden
      if (this.active === true) {
        // bestehende Legendenbilder entfernen
        this.clearLegendDiv();
        // Alle neu erzeugen
        for (layer_idx in map.layers) {
          var layer = map.layers[layer_idx];
          if ( layer instanceof OpenLayers.Layer.WMS && layer.visibility ) {
            // Ein WMS-Layer in OpenLayers kann aus
            // mehreren kommagetrennten WMS-Layern bestehen
            var url_layers_string = layer.params.LAYERS;
            var url_layers = url_layers_string.split(',');
            for(part_idx in url_layers) {
              singlelayer = url_layers[part_idx];
              // hole legende
              var url = layer.url;
              url += ( url.indexOf('?') === -1 ) ? '?' : '';
              url += '&SERVICE=WMS';
              url += '&VERSION=1.1.1';
              url += '&REQUEST=GetLegendGraphic';
              url += '&FORMAT=image/png';
              url += '&LAYER=' + singlelayer;
              var img = document.createElement("img");
              img.src = url;
			  	
				var node = document.createElement("LI");                 // Create a <li> node
				// var textnode = document.createTextNode(url_layers[part_idx]);         // Create a text node
				
				// var textnode = document.createTextNode(map.layers[layer_idx].name);         // Create a text node
				var textnode = document.createTextNode(layer_display_name_array[layer_idx]);         // Edited by prashant
				
				node.appendChild(textnode);                              // Append the text to <li>
				 this.div.appendChild(node);
				 
			  //alert(singlelayer);
			  // this.div.appendChild("<a >" + singlelayer + "</a>");
			//  alert("<a >" + singlelayer + "</a>");
			// alert( img + "<a>" + singlelayer + "</a>");
               this.div.appendChild(img);
			   

            }
          }
        }
      }
    },

    setMap: function(map) {
      // Zun?chst die Elternmethode aufrufen
      OpenLayers.Control.prototype.setMap.apply(this, arguments);
      // Events registrieren
      this.map.events.on({
        "addlayer": this.redraw,
        "changelayer": this.redraw,
        "removelayer": this.redraw,
        "changebaselayer": this.redraw,
        scope: this
      });
      // Control initial aktiveren
      this.active = true;
    },

    deactivate: function() {
      // Zun?chst die Elternmethode aufrufen
      OpenLayers.Control.prototype.deactivate.apply(this, arguments);
      // bestehende Legendenbilder entfernen
      this.clearLegendDiv();
    },

    activate: function( doRedraw ) {
      // Zun?chst die Elternmethode aufrufen
      OpenLayers.Control.prototype.activate.apply(this, arguments);
      // Neuzeichnen des divs
      this.redraw();
    },

    CLASS_NAME: "OpenLayers.Control.Legend"
  }
);