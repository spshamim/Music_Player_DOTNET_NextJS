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
    Name: string;
    Description: string;
    User: {
      Username: string;
      Email: string;
    }
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
    register: updateplaylist,
    handleSubmit: updatesubmit,
    formState: { errors: updateerrors }
    } = useForm();

    useEffect(()=>{
        (async ()=>{
            try {
                if(!toBeSearched){
                    const PResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlist/all`);
                    setSongData(PResponse.data);
                }else{
                    const RResponse = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlist/getn/${toBeSearched}`);
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
            Name: data.name,
            Description: data.description,
            UserId: Number(data.uid)
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/playlist/create`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "Playlist Created!"
            });
    
            addReset();
            setIsCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Create Playlist",
          });
        }
      };

      const updateSongData = async (data: any) => {
        try {
          const requestData = {
            Name: data.plname,
            Description: data.description,
            Id: Number(data.userid)
          };
    
          const ssresponse = await axios.post(`${process.env.NEXT_PUBLIC_API}/api/playlist/update`, requestData);
    
          if (ssresponse) {
            toast({
              variant: "success",
              title: "Playlist Updated!"
            });
    
            setIsUpdateCardVisible(false);
          }
        } catch (error) {
          toast({
            variant: "destructive",
            title: "Failed to Update Playlist",
          });
        }
      };

      const DeletePlaylist = async (id: number) => {
        try{
          const fetcch = await axios.get(`${process.env.NEXT_PUBLIC_API}/api/playlist/delete/${id}`)
          if(fetcch){
            toast({
              variant: "success",
              title: "Playlist Deleted!"
            });
          }else{
            toast({
              variant: "destructive",
              title: "Failed to delete Playlist",
            });
          }
        }catch(error){
          console.error("Error :: ", error);
          toast({
            variant: "destructive",
            title: "Failed to delete Playlist",
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
                placeholder="Search for Playlists"
                {...toWatch('Name')}
                className="appearance-none block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-md shadow-lg ring-2 ring-indigo-500 focus:ring-4 focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            /><br/>

            {/* Viewing the Data */}

            {SongData.length === 0 ? (
                    <h1 className="text-1xl text-red-600 font-bold">Currently No data. Try to search.</h1>
                ) : (
                    <Table>
                    <TableCaption>A list of Playlist.</TableCaption>
                    <TableHeader>
                        <TableRow>
                        <TableHead className="w-[10px]">Playlist Name</TableHead>
                        <TableHead>ID</TableHead>
                        <TableHead>Playlist Description</TableHead>
                        <TableHead>Create by</TableHead>
                        <TableHead>Email</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {SongData.map((data, key) => (
                        <TableRow key={key}>
                            <TableCell className="font-medium">{data.Name}</TableCell>
                            <TableCell>{data.Id}</TableCell>
                            <TableCell>{data.Description}</TableCell>
                            <TableCell>{data.User.Username}</TableCell>
                            <TableCell>{data.User.Email}</TableCell>
                            <TableCell className="text-right">
                                <button type="button"
                                    className="text-white bg-gradient-to-r from-red-400 via-red-500 to-red-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 shadow-lg shadow-red-500/50 dark:shadow-lg dark:shadow-red-800/80 font-medium rounded-lg text-sm px-3 py-1.5 text-center me-2 mb-2"
                                    onClick={()=>{
                                      DeletePlaylist(data.Id);
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
              <CardTitle>Create Playlist</CardTitle>
              <CardDescription>
                Write down the details of the Playlist being added.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form>
                <div className="grid w-full items-center gap-4">
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Name">Playlist Name</Label>
                    <Input
                      type="text"
                      placeholder="Enter Playlist Name"
                      {...addSong("name", {
                          required: "Name is required",
                      })}
                    />
                    {adderrors.name && typeof adderrors.name.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.name.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Description">Playlist Description</Label>
                    <Input
                      type="text"
                      placeholder="Enter Description"
                      {...addSong("description", {
                          required: "description is required",
                      })}
                    />
                    {adderrors.description && typeof adderrors.description.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.description.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="uid">User Id</Label>
                    <Input
                      type="text"
                      placeholder="Enter User Id"
                      {...addSong("uid", {
                          required: "User Id is required",
                      })}
                    />
                    {adderrors.uid && typeof adderrors.uid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{adderrors.uid.message}</span>)}
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
              <CardTitle>Update Playlist</CardTitle>
              <CardDescription>
                Update Playlist data as you want.
              </CardDescription>
            </CardHeader>
            <CardContent>
              <form>
                <div className="grid w-full items-center gap-4">
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Title">Playlist ame</Label>
                    <Input
                      type="text"
                      placeholder="Enter Playlist Name"
                      {...updateplaylist("plname")}
                    />
                    {updateerrors.plname && typeof updateerrors.plname.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.plname.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Artist">Playlist Description</Label>
                    <Input
                      type="text"
                      placeholder="Enter Playlist Description"
                      {...updateplaylist("description")}
                    />
                    {updateerrors.description && typeof updateerrors.description.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.description.message}</span>)}
                  </div>
                  <div className="flex flex-col space-y-1.5">
                    <Label htmlFor="Album">User ID</Label>
                    <Input
                      type="text"
                      placeholder="Enter User ID"
                      {...updateplaylist("userid")}
                    />
                    {updateerrors.userid && typeof updateerrors.userid.message === "string" && (
                    <span className="text-red-700 text-sm font-bold">{updateerrors.userid.message}</span>)}
                  </div>
                </div>
              </form>
            </CardContent>
            <CardFooter className="flex justify-between">
              <Button variant="outline" onClick={() => setIsUpdateCardVisible(false)}>
                Cancel
              </Button>
              <Button onClick={updatesubmit(updateSongData)}>Update</Button>
            </CardFooter>
          </Card>
        </div>
      )}
    </DLayout>
    );
}