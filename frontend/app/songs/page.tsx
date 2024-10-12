"use client"

import axios from "axios";
import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { useToast } from "@/hooks/use-toast";
import DLayout from "@/components/DLayout";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
  } from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
  
interface Song{
    Id: number;
    Title: string;
    Artist: string;
    Album: string;
    Lyrics: string;
    Duration: string;
}
export default function Test() {
    const {register: toWatch , watch} = useForm();
    const { toast } = useToast();
    const [SongData, setSongData] = useState<Song[]>([]);
    const [isCardVisible, setIsCardVisible] = useState(false);
    const [isUpdateCardVisible, setIsUpdateCardVisible] = useState(false);
    var toBeSearched = watch("Name");
    
    const {
        register: addSong,
        handleSubmit: addSumit,
        formState: { errors: adderrors },
        reset: addReset,
      } = useForm();

    const {
    register: updateSong,
    handleSubmit: updateSubmit,
    formState: { errors: updateerrors }
    } = useForm();

    useEffect(()=>{
        (async ()=>{
            try {
                if(!toBeSearched){
                    const PResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/song/all`);
                    setSongData(PResponse.data);
                }else{
                    const RResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/song/view/${toBeSearched}`);
                    setSongData(RResponse.data);
                }
            } catch (error) {
                console.error("Error is :: ", error);
                setSongData([]);
            }
        })()
    },[toBeSearched]);

    const addSongData = async (data: any) => {
        try {
          const requestData = {
            Title: data.title,
            Artist: data.artist,
            Album: data.album,
            Lyrics: data.lyrics,
            Duration: data.duration,
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/song/create`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "Song Created!"
            });
    
            addReset();
            setIsCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Create Song",
          });
        }
      };

      const updateSongData = async (data: any) => {
        try {
          const requestData = {
            Id: data.songid,
            Title: data.utitle,
            Artist: data.uartist,
            Album: data.ualbum,
            Lyrics: data.ulyrics,
            Duration: data.uduration,
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/song/update`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "Song updated!"
            });
    
            setIsUpdateCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to update Song",
          });
        }
      };

      const DeleteSong = async (id: number) => {
        try{
          const fetcch = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/song/delete/${id}`)
          if(fetcch){
            toast({
              variant: "success",
              title: "song Deleted!"
            });
          }else{
            toast({
              variant: "destructive",
              title: "Failed to delete song",
            });
          }
        }catch(error){
          console.error("Error :: ", error);
          toast({
            variant: "destructive",
            title: "Failed to delete Song",
          });
        }
      }

    return (
        <DLayout>
        <div className={`max-w-1xl mx-auto bg-white rounded-lg shadow-lg p-6 flex flex-col items-center ${isCardVisible ? "blur-sm" : ""}`}>
            {/* Button to trigger the card visibility */}
            <Button
                className="text-white bg-gradient-to-r from-green-400 via-green-500 to-green-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-green-300 dark:focus:ring-green-800 shadow-lg shadow-green-500/50 dark:shadow-lg dark:shadow-green-800/80 font-medium rounded-lg text-sm px-20 py-3.5 text-center me-2 mb-2"
                onClick={() => setIsCardVisible(true)}
            >
                Add
            </Button>
            <Button
                className="text-white bg-gradient-to-r from-purple-500 via-purple-600 to-purple-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-purple-300 dark:focus:ring-purple-800 shadow-lg shadow-purple-500/50 dark:shadow-lg dark:shadow-purple-800/80 font-medium rounded-lg text-sm px-16 py-3.5 text-center me-2 mb-2"
                onClick={() => setIsUpdateCardVisible(true)}
            >
                Update
            </Button>
            <br/>
            <input 
                type="text"
                placeholder="Search for Songs"
                {...toWatch('Name')}
                className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md shadow-lg ring-2 ring-indigo-500 focus:ring-4 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            /><br/>

            {/* Viewing the Data */}

            {SongData.length === 0 ? (
                    <h1 className="text-1xl text-red-600 font-bold">No data</h1>
                ) : (
                    <Table>
                    <TableCaption>A list of Song.</TableCaption>
                    <TableHeader>
                        <TableRow>
                        <TableHead className="w-[10px]">Song ID</TableHead>
                        <TableHead>Song Title</TableHead>
                        <TableHead>Song Artist</TableHead>
                        <TableHead>Song Album</TableHead>
                        <TableHead>Song Lyrics</TableHead>
                        <TableHead>Song Duration</TableHead>
                        <TableHead>Action</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {SongData.map((data, key) => (
                        <TableRow key={key}>
                            <TableCell className="font-medium">{data.Id}</TableCell>
                            <TableCell>{data.Title}</TableCell>
                            <TableCell>{data.Artist}</TableCell>
                            <TableCell>{data.Album}</TableCell>
                            <TableCell>{data.Lyrics}</TableCell>
                            <TableCell>{data.Duration}</TableCell>
                            <TableCell className="text-right">
                                <button type="button"
                                    className="text-white bg-gradient-to-r from-red-400 via-red-500 to-red-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 shadow-lg shadow-red-500/50 dark:shadow-lg dark:shadow-red-800/80 font-medium rounded-lg text-sm px-3 py-1.5 text-center me-2 mb-2"
                                    onClick={()=>{
                                      DeleteSong(data.Id);
                                    }}
                                > Delete</button>
                            </TableCell>
                        </TableRow>
                        ))}
                    </TableBody>
                    </Table>
                )}

        {/* End of Viewing Data */}
    </div>
    
    {/* Modal Backdrop & Card */}
    {isCardVisible && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50 backdrop-blur-sm">
          <Card className="w-[500px] z-50 ml-80">
            <CardHeader>
              <CardTitle>Create Songs</CardTitle>
              <CardDescription>
                Write down the details of the song being added.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form>
                <div className="grid w-full items-center gap-4">
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Title">Song Title</Label>
                    <Input
                      type="text"
                      placeholder="Enter Title"
                      {...addSong("title", {
                          required: "Title is required",
                      })}
                    />
                    {adderrors.title && typeof adderrors.title.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.title.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Artist">Song Artist</Label>
                    <Input
                      type="text"
                      placeholder="Enter Artist Name"
                      {...addSong("artist", {
                          required: "Artist is required",
                      })}
                    />
                    {adderrors.artist && typeof adderrors.artist.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.artist.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Album">Song Album</Label>
                    <Input
                      type="text"
                      placeholder="Enter Album Name"
                      {...addSong("album", {
                          required: "Album is required",
                      })}
                    />
                    {adderrors.album && typeof adderrors.album.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.album.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Lyrics">Song Lyrics</Label>
                    <Textarea
                      placeholder="Enter Song Lyrics here"
                      {...addSong("lyrics", {
                          required: "Lyrics is required",
                      })}
                    />
                    {adderrors.lyrics && typeof adderrors.lyrics.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.lyrics.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Duration">Song Duration</Label>
                    <Input
                      type="text"
                      placeholder="Enter Duration"
                      {...addSong("duration", {
                          required: "Duration is required",
                      })}
                    />
                    {adderrors.duration && typeof adderrors.duration.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.duration.message}</span>)}
                  </div>
                </div>
              </form>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="outline" onClick={() => setIsCardVisible(false)}>
                Cancel
              </Button>
              <Button onClick={addSumit(addSongData)}>Add</Button>
            </CardFooter>
          </Card>
        </div>
      )}

        {/* Update Modal Backdrop & Card */}

    {isUpdateCardVisible && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50 backdrop-blur-sm">
          <Card className="w-[500px] z-50 ml-80">
            <CardHeader>
              <CardTitle>Update Song</CardTitle>
              <CardDescription>
                Update songs data as you want.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form>
                <div className="grid w-full items-center gap-4">
                <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Title">Song Id</Label>
                    <Input
                      type="text"
                      placeholder="Enter ID"
                      {...updateSong("songid")}
                    />
                    {updateerrors.songid && typeof updateerrors.songid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.songid.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Title">Song Title</Label>
                    <Input
                      type="text"
                      placeholder="Enter Title"
                      {...updateSong("utitle")}
                    />
                    {updateerrors.utitle && typeof updateerrors.utitle.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.utitle.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Artist">Song Artist</Label>
                    <Input
                      type="text"
                      placeholder="Enter Artist Name"
                      {...updateSong("uartist")}
                    />
                    {updateerrors.uartist && typeof updateerrors.uartist.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.uartist.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Album">Song Album</Label>
                    <Input
                      type="text"
                      placeholder="Enter Album Name"
                      {...updateSong("ualbum")}
                    />
                    {updateerrors.ualbum && typeof updateerrors.ualbum.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.ualbum.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Lyrics">Song Lyrics</Label>
                    <Textarea
                      placeholder="Enter Song Lyrics here"
                      {...updateSong("ulyrics")}
                    />
                    {updateerrors.ulyrics && typeof updateerrors.ulyrics.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.ulyrics.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Duration">Song Duration</Label>
                    <Input
                      type="text"
                      placeholder="Enter Duration"
                      {...updateSong("uduration")}
                    />
                    {updateerrors.uduration && typeof updateerrors.uduration.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.uduration.message}</span>)}
                  </div>
                </div>
              </form>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="outline" onClick={() => setIsUpdateCardVisible(false)}>
                Cancel
              </Button>
              <Button onClick={updateSubmit(updateSongData)}>Update</Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </DLayout>
    );
}