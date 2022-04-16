import React, { useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { unwrapResult } from '@reduxjs/toolkit'
import { addNewListing } from './listingsSlice'
import { postListing } from './listingsSlice'

const AddListingForm = () => {

    const dispatch = useDispatch()

    const [listing, setListing] = useState ({});
    const [addRequestStatus, setAddRequestStatus] = useState('idle')

    function handleChange(evt) {
        const value = evt.target.value;
        setListing({
          ...listing,
          [evt.target.name]: value
        });
    };

    // const canSave =
    // [
    //     listing_Type,
    //     address,
    //     room_Count,
    //     listing_Price,
    //     year_Built,
    //     tour_Date,
    //     floor_Area,
    //     nonusable_Floor_Area,
    //     lot_Area,
    //     form_Of_Lease,
    //     broker_Id
    // ].every(Boolean) && addRequestStatus === 'idle'
    const onSavelistingClicked = async () => {
        debugger;
        // if (canSave) {
          // try {
          //   setAddRequestStatus('pending')
          //   const resultAction = await dispatch(
          //     addNewListing( {
          //       listing_Type: state.listing_Type,
          //       address: state.address,
          //       room_Count: state.room_Count,
          //       listing_Price: state.listing_Price,
          //       year_Built: state.year_Built,
          //       tour_Date: state.tour_Date,
          //       floor_Area: state.floor_Area,
          //       nonusable_Floor_Area: state.nonusable_Floor_Area,
          //       lot_Area: state.lot_Area,
          //       form_Of_Lease: state.form_Of_Lease,
          //       broker_Id: state.broker_Id
          //     } )
          //   )
          //   unwrapResult(resultAction)
          //   setState({
          //       listing_Type: '',
          //       address: '',
          //       room_Count: '',
          //       listing_Price: '',
          //       year_Built: '',
          //       tour_Date: '',
          //       floor_Area: '',
          //       nonusable_Floor_Area: '',
          //       lot_Area: '',
          //       form_Of_Lease: '',
          //       broker_Id: '' 
          //   });
          // } catch (err) {
          //   console.error('Failed to save the post: ', err)
          // } finally {
          //   setAddRequestStatus('idle')
          // }
        // }
        dispatch(postListing({
            "listing_Type": listing.listing_Type,
            "address": listing.address,
            "postal_Area": listing.postal_Area,
            "postal_Code": listing.postal_Code,
            "room_Count": listing.room_Count,
            "listing_Price": listing.listing_Price,
            "year_Built": listing.year_Built,
            "tour_Date": listing.tour_Date,
            "floor_Area": listing.floor_Area,
            "nonusable_Floor_Area": listing.nonusable_Floor_Area,
            "lot_Area": listing.lot_Area,
            "form_Of_Lease": listing.form_Of_Lease,
            "broker_Id": listing.broker_Id
        }));

      }

    return (
        <div class="container">
          <h2 class="m-3">Lägg till bostad</h2>
          <div class="col">
            <label class="form-group col-md-8 my-2">
              Typ av Objekt:
              <input
                type="text"
                name="listing_Type"
                value={listing.listing_Type}
                onChange={handleChange}
                class="form-control"
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Adress:
              <input
                type="text"
                name="address"
                value={listing.address}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Postort:
              <input
                type="text"
                name="postal_Area"
                value={listing.postal_Area}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Postkod
              <input
                type="number"
                name="postal_Code"
                value={listing.postal_Code}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Antal rum:
              <input
                type="number"
                name="room_Count"
                value={listing.room_Count}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class ="form-group col-md-8 my-2">
              Utgångspris:
              <input
                type="number"
                name="listing_Price"
                value={listing.listing_Price}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Byggår:
              <input
                type="number"
                name="year_Built"
                value={listing.year_Built}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Visningsdatum
              <input
                type="date"
                name="tour_Date"
                value={listing.tour_Date}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Boarea:
              <input
                type="number"
                name="floor_Area"
                value={listing.floor_Area}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Biarea:
              <input
                type="number"
                name="nonusable_Floor_Area"
                value={listing.nonusable_Floor_Area}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Tomtarea: 
              <input
                type="number"
                name="lot_Area"
                value={listing.lot_Area}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              upplåtelseform:
              <input
                type="text"
                name="form_Of_Lease"
                value={listing.form_Of_Lease}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <label class="form-group col-md-8 my-2">
              Mäklar id:
              <input
                type="text"
                name="broker_Id"
                value={listing.broker_Id}
                onChange={handleChange}
                class="form-control"
              />
            </label>
            <br/>
            <button class="btn btn-primary" onClick={() => onSavelistingClicked()}>Skicka</button>
          </div>
        </div>
    )
};

export default AddListingForm;