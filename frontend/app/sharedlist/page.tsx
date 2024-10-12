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
  
interface SharedPlaylist{
    PlaylistId : number;
    Playlist: {
        Id : number;
        Name : string;
        Description : string;
    },
    User: {
        Username : string;
        Email : string;
    }
}
export default function Test() {
    const { register, watch } = useForm();
    const { register:shareregister, formState : {errors: shareerrors} , reset:sharereset, handleSubmit:shareSubmit } = useForm();
    const { toast } = useToast();
    const [SharedPlaylistData, setSharedPlaylistData] = useState<SharedPlaylist[]>([]);
    const [isCardVisible, setIsCardVisible] = useState(false);
    var searchbypname = watch("playlistname");
    var searchbyuname = watch("username"); 

    useEffect(()=>{
        (async ()=>{
            try {
                if(searchbypname){
                    const PResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/sharedplaylist/show/${searchbypname}`);
                    setSharedPlaylistData(PResponse.data);
                }else if(searchbyuname){
                    const RResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/SharedPlaylist/get/${searchbyuname}`);
                    setSharedPlaylistData(RResponse.data);
                }else if(!searchbypname || !searchbyuname){
                    const SSResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/sharedplaylist/showall`);
                    setSharedPlaylistData(SSResponse.data);
                }else{
                  setSharedPlaylistData([]);
                }
            } catch (error) {
                console.error("Error is :: ", error);
                setSharedPlaylistData([]);
            }
        })()
    },[searchbypname, searchbyuname]);

    const addShareData = async (data: any) => {
        try {
          const requestData = {
            PlaylistId: data.playlistid,
            UserId: data.userid,
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/sharedplaylist/create`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "Sharing Playlist Successfull!",
              description: `The Playlist Id : ${data.playlistid} has been shared with user Id : ${data.userid}.`,
            });
    
            sharereset();
            setIsCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Share",
            description: "An error occurred while sharing the playlist.",
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
            <div className="flex w-full space-x-4">
                {/* First Search Box with Button */}
                <div className="flex flex-1">
                    <input
                        type="text"
                        placeholder="Search by Playlist Name"
                        {...register('playlistname')}
                        className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-l-md shadow-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    />
                </div>

                {/* Second Search Box with Button */}
                <div className="flex flex-1">
                    <input
                        type="text"
                        placeholder="Search by User Name"
                        {...register('username')}
                        className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-l-md shadow-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    />
                </div>
            </div>

            <br/>

            {/* Viewing the Data */}

            {SharedPlaylistData.length === 0 ? (
                    <h1 className="text-1xl text-red-600 font-bold">Currently No data. Try to search for data.</h1>
                ) : (
                    <Table>
                    <TableCaption>A list of Song from all Playlist.</TableCaption>
                    <TableHeader>
                        <TableRow>
                        <TableHead className="w-[100px]">Playlist ID</TableHead>
                        <TableHead>Playlist Name</TableHead>
                        <TableHead>Playlist Description</TableHead>
                        <TableHead>User Name</TableHead>
                        <TableHead className="text-right">User Email</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {SharedPlaylistData.map((data, key) => (
                        <TableRow key={key}>
                            <TableCell className="font-medium">{data.Playlist.Id}</TableCell>
                            <TableCell>{data.Playlist.Name}</TableCell>
                            <TableCell>{data.Playlist.Description}</TableCell>
                            <TableCell>{data.User.Username}</TableCell>
                            <TableCell className="text-right">{data.User.Email}</TableCell>
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
              <CardTitle>Share Playlist</CardTitle>
              <CardDescription>
                Write down the playlist id and user id to be shared.
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
                      {...shareregister("playlistid", {
                          required: "Playlist ID is required",
                      })}
                    />
                    {shareerrors.playlistid && typeof shareerrors.playlistid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{shareerrors.playlistid.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="UserID">User ID</Label>
                    <Input
                      type="text"
                      placeholder="Enter Song ID"
                      {...shareregister("userid", {
                          required: "User ID is required",
                      })}
                    />
                    {shareerrors.userid && typeof shareerrors.userid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{shareerrors.userid.message}</span>)}
                  </div>
                </div>
              </form>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="outline" onClick={() => setIsCardVisible(false)}>
                Cancel
              </Button>
              <Button onClick={shareSubmit(addShareData)}>Add</Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </DLayout>
    );
}