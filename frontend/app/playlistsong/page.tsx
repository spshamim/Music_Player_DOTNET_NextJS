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
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
  
interface PlayListSong{
    Id : number;
    PlaylistId : number;
    SongId: number;
    Playlist: {
        Id: number;
        Name: string;
        Description: string;
        UserId: number;
    };
    Song: {
        Id: number;
        Title: string;
        Artist: string;
        Album: string;
        Lyrics: string;
        Duration: string;
    };
}
export default function Test() {
    const { register, formState : {errors}, watch, reset, handleSubmit } = useForm();
    const { toast } = useToast();
    const [PlayListSongData, setPlayListSongData] = useState<PlayListSong[]>([]);
    const [isCardVisible, setIsCardVisible] = useState(false);
    var toBeSearched = watch("Name"); 

    useEffect(()=>{
        (async ()=>{
            try {
                if(!toBeSearched){
                    const PResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/getall`);
                    setPlayListSongData(PResponse.data);
                }else{
                    const RResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/show/${toBeSearched}`);
                    setPlayListSongData(RResponse.data);
                }
            } catch (error) {
                console.error("Error is :: ", error);
                setPlayListSongData([]);
            }
        })()
    },[toBeSearched]);

    const addSongToPlaylistSubmitted = async (data: any) => {
        try {
          const requestData = {
            PlaylistId: data.playlistid,
            SongId: data.songid,
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/playlistsong/create`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "Song Added to Playlist Successfully!",
              description: "The song has been added to the playlist.",
            });
    
            reset();
            setIsCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Add Song to Playlist",
            description: "An error occurred while adding the song to the playlist.",
          });
        }
      };

    return (
        <DLayout>
        <div className={`max-w-1xl mx-auto bg-white rounded-lg shadow-lg p-6 flex flex-col items-center ${isCardVisible ? "blur-sm" : ""}`}>
            {/* Button to trigger the card visibility */}
            <Button
                className="text-white bg-gradient-to-r from-purple-500 via-purple-600 to-purple-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-purple-300 dark:focus:ring-purple-800 shadow-lg shadow-purple-500/50 dark:shadow-lg dark:shadow-purple-800/80 font-medium rounded-lg text-sm px-10 py-2.5 text-center me-2 mb-2"
                onClick={() => setIsCardVisible(true)}
            >
                Add
            </Button>
            <br/>
            <input 
                type="text"
                placeholder="Search for"
                {...register('Name')}
                className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md shadow-lg ring-2 ring-indigo-500 focus:ring-4 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            /><br/>

            {/* Viewing the Data */}

            {PlayListSongData.length === 0 ? (
                    <h1 className="text-1xl text-red-600 font-bold">No data</h1>
                ) : (
                    <Table>
                    <TableCaption>A list of Song from all Playlist.</TableCaption>
                    <TableHeader>
                        <TableRow>
                        <TableHead className="w-[100px]">Playlist Name</TableHead>
                        <TableHead>Playlist ID</TableHead>
                        <TableHead>Song ID</TableHead>
                        <TableHead>Song Title</TableHead>
                        <TableHead>Song Artist</TableHead>
                        <TableHead>Song Album</TableHead>
                        <TableHead>Song Lyrics</TableHead>
                        <TableHead className="text-right">Song Duration</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {PlayListSongData.map((data, key) => (
                        <TableRow key={key}>
                            <TableCell className="font-medium">{data.Playlist.Name}</TableCell>
                            <TableCell>{data.Playlist.Id}</TableCell>
                            <TableCell>{data.Song.Id}</TableCell>
                            <TableCell>{data.Song.Title}</TableCell>
                            <TableCell>{data.Song.Artist}</TableCell>
                            <TableCell>{data.Song.Album}</TableCell>
                            <TableCell>{data.Song.Lyrics}</TableCell>
                            <TableCell className="text-right">{data.Song.Duration}</TableCell>
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
          <Card className="w-[350px] z-50">
            <CardHeader>
              <CardTitle>Add Songs to Playlist</CardTitle>
              <CardDescription>
                Write down the playlist id and song id you want to add.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form>
                <div className="grid w-full items-center gap-4">
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="PlaylistID">Playlist ID</Label>
                    <Input
                      type="text"
                      placeholder="Enter Playlist ID"
                      {...register("playlistid", {
                          required: "Playlist ID is required",
                      })}
                    />
                    {errors.playlistid && typeof errors.playlistid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{errors.playlistid.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="SongID">Song ID</Label>
                    <Input
                      type="text"
                      placeholder="Enter Song ID"
                      {...register("songid", {
                          required: "Song ID is required",
                      })}
                    />
                    {errors.songid && typeof errors.songid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{errors.songid.message}</span>)}
                  </div>
                </div>
              </form>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="outline" onClick={() => setIsCardVisible(false)}>
                Cancel
              </Button>
              <Button onClick={handleSubmit(addSongToPlaylistSubmitted)}>Add</Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </DLayout>
    );
}