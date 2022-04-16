import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import { client } from '../../api/client'

let initialState = {
    listings: [],
    status: 'idle',
    error: null
};

export const fetchListings = createAsyncThunk('listings/fetchListings', async () => {  
    const response = await client.get('https://localhost:44382/api/listings')    
    return response
});

export const addNewListing = createAsyncThunk(
    'listings/addNewListing',
    async initialListing =>{
        const response = await client.post('https://localhost:44382/api/listings', {listing: initialListing});
        return response.listing;
    }
)

export const postListing = createAsyncThunk('listings/postlisting', async (props) =>
{  
    const response = await client.post('https://localhost:44382/api/listings', 
    {"listing_Type": props.listing_Type,
    "address": props.address,
    "postal_Area": props.postal_Area,
    "postal_Code": props.postal_Code,
    "room_Count": props.room_Count,
    "listing_Price": props.listing_Price,
    "year_Built": props.year_Built,
    "tour_Date": props.tour_Date,
    "floor_Area": props.floor_Area,
    "nonusable_Floor_Area": props.nonusable_Floor_Area,
    "lot_Area": props.lot_Area,
    "form_Of_Lease": props.form_Of_Lease,
    "broker_Id": props.broker_Id});

    console.log(response);
    return response;
});

export const putListing = createAsyncThunk('listings/putlisting', async (props) => 
{
    const response = await client.put(`https://localhost:44382/api/listings/${props.listing_Id}`, 
    {
    "listing_Id": props.listing_Id,
    "listing_Type": props.listing_Type,
    "address": props.address,
    "postal_Area": props.postal_Area,
    "postal_Code": props.postal_Code,
    "room_Count": props.room_Count,
    "listing_Price": props.listing_Price,
    "year_Built": props.year_Built,
    "tour_Date": props.tour_Date,
    "floor_Area": props.floor_Area,
    "nonusable_Floor_Area": props.nonusable_Floor_Area,
    "lot_Area": props.lot_Area,
    "form_Of_Lease": props.form_Of_Lease,
    "broker_Id": props.broker_Id});

    console.log(response);
    return response;
})

export const deleteListing = createAsyncThunk('listings/deletelisting' , async (id) => 
{
    const response = await client.delete(`https://localhost:44382/api/listings/${id}`);
    // ,{token : _token}

    console.log(id);
    return response;
});

const listingsSlice = createSlice({
    name: 'listings',
    initialState,
    reducers: {},
    extraReducers:
        {
            [fetchListings.pending]: (state, action) =>
            {
                state.status = 'loading';
            },
            [fetchListings.fulfilled]: (state, action) =>
            {
                state.listings = [];
                state.status = 'succeeded';
                state.listings = state.listings.concat(action.payload);
            },
            [fetchListings.rejected]: (state, action) =>
            {
                state.status = 'failed';
                state.error = action.error.message;
            },
            [postListing.fulfilled]: (state, action) =>
            {
                state.listings.push(action.payload)
            },
            [putListing.fulfilled]: (state, action) =>
            {
                state.listings.push(action.payload)
            },
            [deleteListing.fulfilled]: (state, action) =>
            {
                console.log(action.payload);
            }

            
        }
});

export default listingsSlice.reducer;

export const selectAllListings = state => state.listings.listings;

export const selectListingById = (state, listingId) =>
  state.listings.listings.find(listing => listing.listing_Id === listingId);
